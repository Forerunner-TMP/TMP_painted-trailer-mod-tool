using System;
using System.IO;

namespace PaintedTrailerTool
{
    class Program
    {
        static void Main(string[] options)
        {
            
            string localpath;
            string paintName;
            string latestInput;
            bool useOldTrailers;
            bool latestColourValid = false;
            float colourR = 0;
            float colourG = 0;
            float colourB = 0;
            string blankSii;
            string newSii;
            int currentIndex = 0;
            string baseCompany = "company_permanent: company.permanent.<intname>\n{\n\tname: \"<intname>\"\n\tsort_name: \"<intname>\"\n\ttrailer_look: default\n}";
            string[] companies = { "agronord.dlc_north", "aria_fd_albg.dlc_north", "aria_fd_esbj.dlc_north", "aria_fd_jnpg.dlc_north", "aria_fd_trbg.dlc_north", "batisse_hs", "bcp", "bhv.dlc_north", "bhv", "bjork.dlc_north", "cont_port.dlc_north", "drekkar.dlc_north", "euroacres", "eurogoodies", "fcp", "fle", "gnt.dlc_north", "ika_bohag.dlc_north", "ika_bohag", "itcc", "kaarfor", "konstnr.dlc_north", "konstnr_br.dlc_north", "konstnr_hs.dlc_north", "konstnr_wind.dlc_north", "lisette_log", "lkwlog", "marina.dlc_north", "ms_stein.dlc_north", "nbfc", "nord_crown.dlc_north", "nord_sten.dlc_north", "norrsken.dlc_north", "norr_food.dlc_north", "ns_chem.dlc_north", "ns_oil.dlc_north", "polarislines.dlc_north", "polar_fish.dlc_north", "posped", "quarry", "renar.dlc_north", "sag_tre.dlc_north", "sanbuilders", "scania_dlr", "scania_fac.dlc_north", "sellplan", "skoda", "stokes", "tradeaux", "trameri", "transinet", "tree_et", "vitas_pwr.dlc_north", "voitureux", "volvo_dlr", "volvo_fac.dlc_north", "vpc.dlc_north", "wgcc" };
            string[] trailerNames = { "fridge", "krone_coolliner", "krone_profiliner", "schmitz_universal" };
            string[] trailers = { "cement", "chemical_cistern", "food_cistern", "fuel_cistern", "livestock" };
            string[] oldTrailers = { "reefer/chassis", "reefer/chassis_a", "container/chassis", "container/chassis" };
            string[] newTrailers = { "krone/fridge", "krone/coolliner", "krone/profiliner", "schmitz/universal" };
            localpath = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                using (var streamReader = new StreamReader(localpath + "emptytrailer.sii"))
                {
                    blankSii = streamReader.ReadToEnd();
                }
            }
            catch
            {
                Console.Write("emptytrailer.sii is missing! Presee enter to close program.");
                Console.Read();
                return;
            }
            if (options.Length > 0)
            {
                if (options[0] == "true")
                {
                    Console.Write(localpath);
                    Console.Read();
                }
            }
            Console.Write("Please enter the name of the new paint mod: ");
            paintName = Console.ReadLine();
            Console.Write("\n");
            Console.Write("Do you want to use the ETS1 Trailers (y/n)? ");
            latestInput = Console.ReadLine();
            if (latestInput == "y")
            {
                useOldTrailers = true;
            } else if (latestInput == "n") {
                useOldTrailers = false;
            } else
            {
                Console.Write("Invalid value entered. Assumeing no.");
                useOldTrailers = false;
            }
            Console.Write("\n");
            Console.Write("Trailer colour: \n");
            while (latestColourValid == false)
            {
                Console.Write("Please enter the R value for the colour (0 - 255): ");
                try
                {
                    colourR = float.Parse(Console.ReadLine());
                    if (colourR < 0)
                    {
                        Console.Write("Invalid value entered (too low). \n");
                    }
                    else if (colourR > 255)
                    {
                        Console.Write("Invalid value entered (too high). \n");
                    }
                    else
                    {
                        latestColourValid = true;
                        Console.Write("\n");
                    }
                }
                catch
                {
                    Console.Write("Invalid value entered. \n");
                }
            }
            latestColourValid = false;
            while (latestColourValid == false)
            {
                Console.Write("Please enter the G value for the colour (0 - 255): ");
                try
                {
                    colourG = float.Parse(Console.ReadLine());
                    if (colourG < 0)
                    {
                        Console.Write("Invalid value entered (too low). \n");
                    }
                    else if (colourG > 255)
                    {
                        Console.Write("Invalid value entered (too high). \n");
                    }
                    else
                    {
                        latestColourValid = true;
                        Console.Write("\n");
                    }
                }
                catch
                {
                    Console.Write("Invalid value entered. \n");
                }
            }
            latestColourValid = false;
            while (latestColourValid == false)
            {
                Console.Write("Please enter the B value for the colour (0 - 255): ");
                try
                {
                    colourB = float.Parse(Console.ReadLine());
                    if (colourB < 0)
                    {
                        Console.Write("Invalid value entered (too low). \n");
                    }
                    else if (colourB > 255)
                    {
                        Console.Write("Invalid value entered (too high). \n");
                    }
                    else
                    {
                        latestColourValid = true;
                        Console.Write("\n");
                    }
                }
                catch
                {
                    Console.Write("Invalid value entered. \n");
                }
            }
            colourR = colourR / 255;
            colourG = colourG / 255;
            colourB = colourB / 255;
            Directory.CreateDirectory(localpath + paintName + "/def/vehicle/trailer");
            Directory.CreateDirectory(localpath + paintName + "/def/company");
            foreach (string currentTrailer in trailers)
            {
                newSii = blankSii;
                newSii = newSii.Replace("<trailer>", currentTrailer+"/chassis");
                if (currentTrailer == "chemical_cistern")
                {
                    newSii = newSii.Replace("<intname>", "chcistern");
                }
                else
                {
                    newSii = newSii.Replace("<intname>", currentTrailer);
                }
                newSii = newSii.Replace("<r>", colourR.ToString().Replace(",","."));
                newSii = newSii.Replace("<g>", colourG.ToString().Replace(",","."));
                newSii = newSii.Replace("<b>", colourB.ToString().Replace(",","."));
                using (StreamWriter siiout = new StreamWriter(localpath + paintName + "/def/vehicle/trailer/" + currentTrailer + ".sii"))
                {
                    siiout.Write(newSii);
                }
                Console.Write(currentTrailer + " has been written\n");
            }
            if (useOldTrailers == true)
            {
                foreach (string currentTrailer in oldTrailers)
                {
                    newSii = blankSii;
                    newSii = newSii.Replace("<trailer>", currentTrailer);
                    newSii = newSii.Replace("<intname>", trailerNames[currentIndex].Replace("_","."));
                    newSii = newSii.Replace("<r>", colourR.ToString().Replace(",","."));
                    newSii = newSii.Replace("<g>", colourG.ToString().Replace(",","."));
                    newSii = newSii.Replace("<b>", colourB.ToString().Replace(",","."));
                    using (StreamWriter siiout = new StreamWriter(localpath + paintName + "/def/vehicle/trailer/" + trailerNames[currentIndex] + ".sii"))
                    {
                        siiout.Write(newSii);
                    }
                    Console.Write(currentTrailer + " has been written as: " + trailerNames[currentIndex] + "\n");
                    currentIndex = currentIndex + 1;
                }
            }
            else
            {
                foreach (string currentTrailer in newTrailers)
                {
                    newSii = blankSii;
                    newSii = newSii.Replace("<trailer>", currentTrailer + "/chassis");
                    newSii = newSii.Replace("<intname>", trailerNames[currentIndex].Replace("_", "."));
                    newSii = newSii.Replace("<r>", colourR.ToString().Replace(",","."));
                    newSii = newSii.Replace("<g>", colourG.ToString().Replace(",","."));
                    newSii = newSii.Replace("<b>", colourB.ToString().Replace(",","."));
                    using (StreamWriter siiout = new StreamWriter(localpath + paintName + "/def/vehicle/trailer/" + trailerNames[currentIndex] + ".sii"))
                    {
                        siiout.Write(newSii);
                    }
                    Console.Write(currentTrailer + " has been written as: " + trailerNames[currentIndex] + "\n");
                    currentIndex = currentIndex + 1;
                }
            }
            foreach (string currentCompany in companies)
            {
                using (StreamWriter siiout = new StreamWriter(localpath + paintName + "/def/company/" + currentCompany + ".sii"))
                {
                    siiout.Write(baseCompany.Replace("<intname>",currentCompany.Replace(".dlc_north","")));
                }
            }
            Console.Write("\nTrailer mod successfully created. press enter to exit.");
            Console.Read();
        }
    }
}
