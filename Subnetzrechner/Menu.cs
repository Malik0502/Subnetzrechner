﻿using Service;
using Service.Interfaces;

namespace Subnetzrechner
{
    public class Menu : IMenu
    {
        private readonly IUserInteraction information;
        private readonly IParser parser;

        public Menu(IUserInteraction information, IParser parser)
        {
            this.information = information;
            this.parser = parser;
        }

        // Komplettes Menü entfernen und eine Commandline Anwendung daraus machen.
        public void StartMenu()
        {
            IUserInteraction information = this.information;

            while (true)
            {
                try
                {
                    Console.WriteLine("1. Subnetzrechner");
                    Console.WriteLine("2. Asymmetrischer Subnetzrechner");
                    int menuInput = int.Parse(Console.ReadLine()!);
                    
                    Console.Clear();

                    switch (menuInput)
                    {
                        case 1:
                            Console.WriteLine("IP-Adresse:");
                            string ipAdressInput = Console.ReadLine() ?? string.Empty;

                            Console.WriteLine("Subnetzmaske:");
                            string subnetmaskInput = Console.ReadLine() ?? string.Empty;

                            Console.WriteLine("Anzahl Subnetze:");
                            int subnetAmountInput = int.Parse(Console.ReadLine()!);

                            SubnetEntity inputEntity = new()
                            {
                                IPAdress = ipAdressInput,
                                SubnetMask = subnetmaskInput,
                                SubnetAmount = subnetAmountInput
                            };

                            Console.Clear();

                            information.ShowAvailableSubnets(inputEntity);

                            break;

                        case 2:
                            List<int> inputHostAmount = new List<int>();

                            Console.WriteLine("IP-Adresse:");
                            string inputIpAdress = Console.ReadLine() ?? string.Empty;

                            Console.WriteLine("Anzahl Subnetze:");
                            int inputSubnetAmount = int.Parse(Console.ReadLine()!);


                            for (int i = 0; i < inputSubnetAmount; i++)
                            {
                                Console.WriteLine($"Anzahl Host für Subnetz {i + 1}");
                                int input = int.Parse(Console.ReadLine()!);
                                inputHostAmount.Add(input);
                                Console.Clear();
                            }

                            AsymSubnetEntity asymInputEntity = new()
                            {
                                IPAdress = parser.StringToBinary(inputIpAdress),
                                SubnetAmount = inputSubnetAmount,
                                HostAmount = inputHostAmount,
                            };

                            information.ShowAvailableAsymSubnets(asymInputEntity);

                            break;
                    }


                    Console.Read();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bitte überprüfen Sie ihre Werte");
                    Console.WriteLine($"Fehlercode: {ex}");
                }
            }
        }
    }
}
