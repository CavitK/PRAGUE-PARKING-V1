string[] parkingGarage = new string[100];

bool Running = true;
while (Running)
{
    Console.WriteLine("\n=== PRAGUE PARKING ===");
    Console.WriteLine("1. Visa parkeringsplatser");
    Console.WriteLine("2. Parkera bil");
    Console.WriteLine("3. Parkera motorcykel");
    Console.WriteLine("4. Flytta fordon");
    Console.WriteLine("5. Hämta fordon");
    Console.WriteLine("6. Sök efter fordon");
    Console.WriteLine("0. Avsluta");

    Console.Write("Gör ditt val: ");
    string choise = Console.ReadLine();

    switch (choise)
    {
        case "1":
            Console.WriteLine("\nVisar parkeringsplatser:");
            for (int i = 0; i < parkingGarage.Length; i++)
            {
                Console.WriteLine("Plats: " + (i + 1) + ": " + (parkingGarage[i] ?? "Ledig plats"));
            }
            break;

        case "2":
            Console.WriteLine("\nParkera bil");
            Console.WriteLine("Ange registeringsnummer: ");

            string Regnumber = Console.ReadLine().Trim();
            int foundspot = -1;

            for (int i = 0; i < parkingGarage.Length; i++)
            {
                if (parkingGarage[i] == null)
                {
                    foundspot = i;
                    break;
                }
            }
            if (foundspot != -1)
            {
                parkingGarage[foundspot] = "CAR#" + Regnumber;
                Console.WriteLine("Kör bilen till parkeringsplats: " + (foundspot + 1));
            }
            else
            {
                Console.WriteLine("Parkeringen är full.");
            }
            break;

        case "3":
            Console.WriteLine("\nParkera motorcykel");
            Console.WriteLine("Ange registeringsnummer: ");

            string MCregnumber = Console.ReadLine().Trim();
            int MCfoundspot = -1;
            for (int i = 0; i < parkingGarage.Length; i++)
            {
                if (parkingGarage[i] == null ||
                    (parkingGarage[i].StartsWith("MC#") &&
                    !parkingGarage[i].Contains("|MC#")))
                {
                    MCfoundspot = i;
                    if (parkingGarage[i] == null)
                    {
                        parkingGarage[MCfoundspot] = "MC#" + MCregnumber;
                    }
                    else
                    {
                        parkingGarage[MCfoundspot] = parkingGarage[MCfoundspot] + "|MC#" + MCregnumber;
                    }
                    Console.WriteLine("Kör motorcykeln till parkeringsplats: " + (MCfoundspot + 1));
                    break;
                }
            }
            if (MCfoundspot == -1)
            {
                Console.WriteLine("Det finns inge ledig plats till motorcykeln.");
            }
            break;

        case "4":
            Console.WriteLine("\nFlytta fordon");
            Console.WriteLine("Ange registreringsnummer:");

            string Moveregnumber = Console.ReadLine().Trim();

            int Movefoundspot = -1;

            for (int i = 0; i < parkingGarage.Length; i++)
            {
                if (parkingGarage[i] != null && parkingGarage[i].Contains(Moveregnumber))
                {
                    Movefoundspot = i;
                    break;
                }
            }


            if (Movefoundspot != -1)
            {
                string[] vehicles = parkingGarage[Movefoundspot].Split('|');

                Console.WriteLine("Ange ny parkeringsplats:");

                if (int.TryParse(Console.ReadLine(), out int Newspot))
                {
                    Newspot = Newspot - 1;


                    if (Newspot < 0 || Newspot >= parkingGarage.Length)
                    {
                        Console.WriteLine("Parkeringsplatsen måste vara mellan 1 och 100.");
                    }
                    else if (Newspot == Movefoundspot)
                    {
                        Console.WriteLine("Fordonet står redan på den platsen.");
                    }
                    else
                    {

                        string vehicleToMove = "";
                        string remainingVehicle = null;

                        if (vehicles.Length == 2)
                        {
                            bool movingFirst = vehicles[0].Contains(Moveregnumber);
                            vehicleToMove = movingFirst ? vehicles[0] : vehicles[1];
                            remainingVehicle = movingFirst ? vehicles[1] : vehicles[0];
                        }
                        else
                        {
                            vehicleToMove = vehicles[0];
                        }

                        string destination = parkingGarage[Newspot];
                        bool isMC = vehicleToMove.StartsWith("MC#");

                        if (destination == null)
                        {
                            parkingGarage[Newspot] = vehicleToMove;
                            parkingGarage[Movefoundspot] = remainingVehicle;
                            Console.WriteLine("Fordonet har flyttats till parkeringsplats: " + (Newspot + 1));
                        }

                        else if (isMC && destination.StartsWith("MC#") && !destination.Contains("|"))
                        {
                            parkingGarage[Newspot] = destination + "|" + vehicleToMove;
                            parkingGarage[Movefoundspot] = remainingVehicle;
                            Console.WriteLine("Motorcykeln har flyttats till parkeringsplats: " + (Newspot + 1));
                        }
                        else
                        {
                            Console.WriteLine("Den nya parkeringsplatsen är upptagen.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning. Ange ett tal mellan 1 och 100.");
                }
            }
            else
            {
                Console.WriteLine("Fordonet hittades inte.");
            }

            break;

        case "5":
            Console.WriteLine("\nHämta fordon");
            Console.WriteLine("Ange registreringsnummer:");

            string Pickupregnumber = Console.ReadLine().Trim();

            int Pickupfoundspot = -1;

            for (int i = 0; i < parkingGarage.Length; i++)
            {
                if (parkingGarage[i] != null &&
                    parkingGarage[i].Contains(Pickupregnumber))
                {
                    Pickupfoundspot = i;
                    break;
                }
            }

            if (Pickupfoundspot != -1)
            {
                Console.WriteLine(
                    "Fordonet finns på parkeringsplats: "
                    + (Pickupfoundspot + 1));

                string[] vehicles =
                    parkingGarage[Pickupfoundspot].Split('|');

                if (vehicles.Length == 2)
                {
                    if (vehicles[0].Contains(Pickupregnumber))
                    {
                        parkingGarage[Pickupfoundspot] = vehicles[1];
                    }
                    else
                    {
                        parkingGarage[Pickupfoundspot] = vehicles[0];
                    }
                }
                else
                {
                    parkingGarage[Pickupfoundspot] = null;
                }

                Console.WriteLine("Fordonet är hämtat.");
            }
            else
            {
                Console.WriteLine("Fordonet hittades inte.");
            }

            break;

        case "6":
            Console.WriteLine("\nSök efter fordon");
            Console.WriteLine("Ange registreringsnummer:");

            string Searchregnumber = Console.ReadLine().Trim();

            int Searchfoundspot = -1;

            for (int i = 0; i < parkingGarage.Length; i++)
            {
                if (parkingGarage[i] != null &&
                    parkingGarage[i].Contains(Searchregnumber))
                {
                    Searchfoundspot = i;
                    break;
                }
            }

            if (Searchfoundspot != -1)
            {
                Console.WriteLine(
                    "Fordonet finns på parkeringsplats: "
                    + (Searchfoundspot + 1));
            }
            else
            {
                Console.WriteLine("Fordonet hittades inte.");
            }

            break;

        case "0":
            Console.WriteLine("Programmet avslutas.");
            Running = false;
            break;

        default:
            Console.WriteLine("Ogiltigt menyval.");
            break;
    }
}
