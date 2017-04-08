using EasyTrainTickets.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Domain.Data
{
    public static class ConnectionsGenerator
    {
        public static List<Connection> PerDay(DateTime day, IEasyTrainTicketsDbEntities db)
        {
            Random rand = new Random();
            string seats = @"11;12;13;14;15;16;17;18;21;22;23;24;25;26;27;28;31;32;33;34;35;36;37;38;41;42;43;44;45;46;47;48;51;52;53;54;55;56;57;58;61;62;63;64;65;66;67;68;71;72;73;74;75;76;77;78;81;82;83;84;85;86;87;88;91;92;93;94;95;96;97;98;101;102;103;104;105;106;107;108";
            string expressSeats = @"11;12;13;14;15;16;21;22;23;24;25;26;31;32;33;34;35;36;41;42;43;44;45;46;51;52;53;54;55;56;61;62;63;64;65;66;71;72;73;74;75;76;81;82;83;84;85;86;91;92;93;94;95;96;101;102;103;104;105;106";
            Train train;
            Train express;
            List<Route> routes;
            train = db.Trains.Where(t => t.Type == "Pośpieszny").First();
            express = db.Trains.Where(t => t.Type == "Ekspres").First();
            routes = db.Routes.ToList();
            List<Connection> connections = new List<Connection>();
            connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa", "Łódź", "Wrocław", "Jelenia Góra" }, new int[] { 0, 15, 10, 5, 0 }, day, 5, 25, routes, train, "Orzeszkowa", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa" }, new int[] { 0, 0 }, day, 7, 43, routes, train, "Branicki", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa", "Łódź" }, new int[] { 0, 15, 0 }, day, 6, 54, routes, train, "Zamenhof", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa", "Łódź", "Wrocław" }, new int[] { 0, 10, 5, 0 }, day, 14, 11, routes, train, "Konopnicka", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa", "Piotrków Tryb.", "Częstochowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 30, 1, 1, 5, 0 }, day, 9, 47, routes, train, "Ondraszek", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa", "Łódź" }, new int[] { 0, 10, 0 }, day, 16, 10, routes, train, "Słonimski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa", "Łódź" }, new int[] { 0, 10, 0 }, day, 20, 12, routes, train, "Leśmian", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa", "Łódź" }, new int[] { 0, 10, 0 }, day, 11, 43, routes, train, "Nałkowska", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Białystok", "Olsztyn", "Gdańsk", "Słupsk", "Szczecin" }, new int[] { 0, 25, 15, 5, 0 }, day, 5, 50, routes, train, "Rybak", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Białystok", "Olsztyn", "Gdańsk" }, new int[] { 0, 25, 0 }, day, 13, 31, routes, train, "Biebrza", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Częstochowa", "Piotrków Tryb.", "Warszawa", "Białystok" }, new int[] { 0, 10, 3, 1, 10, 0 }, day, 11, 31, routes, train, "Ondraszek", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Częstochowa", "Piotrków Tryb.", "Warszawa", "Olsztyn" }, new int[] { 0, 10, 4, 1, 10, 0 }, day, 12, 54, routes, train, "Kormoran", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Włoszczowa", "Warszawa", "Gdańsk" }, new int[] { 0, 5, 2, 5, 0 }, day, 15, 56, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Włoszczowa", "Warszawa", "Gdańsk" }, new int[] { 0, 4, 1, 5, 0 }, day, 8, 0, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Włoszczowa", "Warszawa" }, new int[] { 0, 8, 2, 0 }, day, 5, 15, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Częstochowa", "Piotrków Tryb.", "Warszawa", "Lublin" }, new int[] { 0, 10, 6, 2, 15, 0 }, day, 14, 58, routes, train, "Pilecki", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Częstochowa", "Piotrków Tryb.", "Warszawa" }, new int[] { 0, 10, 6, 1, 0 }, day, 17, 7, routes, train, "Korfanty", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Częstochowa", "Piotrków Tryb.", "Warszawa" }, new int[] { 0, 15, 6, 2, 0 }, day, 4, 36, routes, train, "Górnik", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Bydgoszcz", "Toruń", "Kutno", "Warszawa" }, new int[] { 0, 7, 3, 0 }, day, 3, 23, routes, train, "Ogiński", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Bydgoszcz", "Toruń", "Kutno", "Warszawa" }, new int[] { 0, 8, 4, 0 }, day, 8, 18, routes, train, "Kujawiak", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Bydgoszcz", "Toruń", "Kutno", "Warszawa" }, new int[] { 0, 7, 2, 0 }, day, 16, 3, routes, train, "Kopernik", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Bydgoszcz", "Toruń", "Kutno", "Warszawa", "Lublin", "Rzeszów" }, new int[] { 0, 7, 4, 10, 15, 0 }, day, 5, 46, routes, train, "Staszic", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Częstochowa", "Kraków" }, new int[] { 0, 0 }, day, 4, 59, routes, train, "Pogoria", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Częstochowa", "Piotrków Tryb.", "Warszawa", "Lublin" }, new int[] { 0, 1, 24, 0 }, day, 5, 10, routes, train, "Czartoryski", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Gdańsk", "Olsztyn", "Białystok" }, new int[] { 0, 15, 0 }, day, 8, 0, routes, train, "Biebrza", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Poznań", "Zielona Góra" }, new int[] { 0, 10, 10, 0 }, day, 14, 5, routes, train, "Bachus", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Poznań" }, new int[] { 0, 10, 0 }, day, 18, 36, routes, train, "Bałtyk", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Poznań", "Wrocław" }, new int[] { 0, 3, 3, 0 }, day, 6, 30, routes, train, "Piast", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Poznań", "Wrocław" }, new int[] { 0, 3, 13, 0 }, day, 16, 33, routes, train, "Mieszko", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa" }, new int[] { 0, 0 }, day, 4, 50, routes, train, "Rataj", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Włoszczowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 5, 2, 4, 0 }, day, 6, 1, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Włoszczowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 5, 1, 4, 0 }, day, 15, 53, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 5, 2, 0 }, day, 9, 48, routes, express, "Ex Sobieski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 5, 2, 0 }, day, 7, 53, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 5, 2, 0 }, day, 13, 54, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Kraków" }, new int[] { 0, 5, 0 }, day, 6, 54, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Częstochowa", "Opole", "Wrocław" }, new int[] { 0, 5, 2, 3, 0 }, day, 8, 0, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Toruń", "Kutno", "Warszawa", "Lublin", "Rzeszów", "Przemyśl" }, new int[] { 0, 16, 5, 1, 10, 17, 26, 0 }, day, 11, 50, routes, train, "Kochanowski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Kraków" }, new int[] { 0, 5, 0 }, day, 10, 54, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Kraków" }, new int[] { 0, 5, 0 }, day, 12, 54, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Częstochowa", "Opole", "Wrocław" }, new int[] { 0, 5, 4, 2, 0 }, day, 14, 0, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Kraków", "Rzeszów" }, new int[] { 0, 5, 10, 0 }, day, 14, 54, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Kraków" }, new int[] { 0, 5, 0 }, day, 16, 54, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Lublin" }, new int[] { 0, 10, 0 }, day, 17, 45, routes, train, "Neptun", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Poznań", "Wrocław" }, new int[] { 0, 3, 14, 0 }, day, 8, 18, routes, train, "Pomorzanin", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Włoszczowa", "Kraków" }, new int[] { 0, 10, 2, 0 }, day, 19, 49, routes, train, "Karpaty", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Słupsk", "Szczecin" }, new int[] { 0, 5, 0 }, day, 5, 55, routes, train, "Albatros", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Poznań", "Wrocław", "Jelenia Góra" }, new int[] { 0, 10, 25, 15, 0 }, day, 23, 10, routes, train, "Sudety", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Toruń", "Kutno", "Warszawa" }, new int[] { 0, 7, 2, 4, 0 }, day, 17, 36, routes, train, "Doker", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Toruń", "Kutno", "Łódź", "Piotrków Tryb.", "Częstochowa", "Katowice" }, new int[] { 0, 8, 2, 2, 2, 1, 2, 0 }, day, 13, 30, routes, train, "Hutnik", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Jelenia Góra", "Wrocław", "Łódź", "Warszawa", "Białystok" }, new int[] { 0, 10, 10, 15, 0 }, day, 13, 5, routes, train, "Orzeszkowa", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Jelenia Góra", "Wrocław", "Opole", "Częstochowa", "Warszawa" }, new int[] { 0, 10, 2, 5, 0 }, day, 7, 9, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Jelenia Góra", "Wrocław", "Opole", "Katowice", "Kraków", "Rzeszów", "Przemyśl" }, new int[] { 0, 10, 2, 5, 12, 25, 0 }, day, 8, 34, routes, train, "Mehoffer", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Jelenia Góra", "Wrocław", "Łódź", "Warszawa" }, new int[] { 0, 10, 15, 0 }, day, 16, 30, routes, train, "Asnyk", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Jelenia Góra", "Wrocław", "Poznań", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 10, 15, 12, 0 }, day, 20, 32, routes, train, "Sudety", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Lublin", "Warszawa", "Gdańsk" }, new int[] { 0, 10, 0 }, day, 12, 15, routes, train, "Neptun", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Lublin", "Warszawa", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 15, 5, 15, 0 }, day, 5, 0, routes, train, "Gałczyński", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Lublin", "Warszawa", "Piotrków Tryb.", "Częstochowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 10, 2, 5, 15, 0 }, day, 5, 48, routes, train, "Pilecki", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Lublin", "Warszawa", "Łódź", "Wrocław" }, new int[] { 0, 15, 10, 0 }, day, 9, 50, routes, train, "Słowacki", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Lublin", "Radom", "Kielce", "Kraków" }, new int[] { 0, 5, 6, 0 }, day, 18, 46, routes, train, "Jagiełło", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Lublin", "Radom", "Kielce", "Kraków" }, new int[] { 0, 2, 3, 0 }, day, 6, 43, routes, train, "Długosz", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Lublin", "Radom", "Kielce", "Włoszczowa", "Katowice", "Opole", "Wrocław" }, new int[] { 0, 2, 3, 4, 10, 4, 0 }, day, 8, 20, routes, train, "Bolko", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Lublin", "Radom", "Kielce", "Włoszczowa", "Katowice", "Opole", "Wrocław" }, new int[] { 0, 2, 3, 4, 10, 4, 0 }, day, 12, 23, routes, train, "Sztygar", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Lublin", "Radom", "Kielce", "Włoszczowa", "Katowice" }, new int[] { 0, 2, 3, 4, 0 }, day, 16, 25, routes, train, "Godula", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Lublin", "Warszawa", "Piotrków Tryb.", "Częstochowa" }, new int[] { 0, 10, 1, 0 }, day, 17, 50, routes, train, "Czartoryski", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, day, 20, 32, routes, train, "Wokulski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, day, 17, 32, routes, train, "Prząśniczka", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, day, 15, 32, routes, train, "Tuwim", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, day, 11, 31, routes, train, "Rzecki", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, day, 11, 30, routes, train, "Jagna", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, day, 6, 47, routes, train, "Łęcka", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, day, 6, 40, routes, train, "Łodzianin", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, day, 5, 44, routes, train, "Zosia", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, day, 9, 32, routes, train, "Telimena", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, day, 5, 12, routes, train, "Soplica", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa", "Białystok" }, new int[] { 0, 15, 0 }, day, 16, 22, routes, train, "Zamenhof", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa", "Białystok" }, new int[] { 0, 15, 0 }, day, 8, 32, routes, train, "Słonimski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa", "Białystok" }, new int[] { 0, 15, 0 }, day, 5, 6, routes, train, "Leśmian", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa", "Białystok" }, new int[] { 0, 15, 0 }, day, 12, 22, routes, train, "Nałkowska", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa", "Olsztyn" }, new int[] { 0, 15, 0 }, day, 6, 29, routes, train, "Mazury", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 5, 10, 0 }, day, 6, 46, routes, train, "Włókniarz", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Łódź", "Piotrków Tryb.", "Częstochowa", "Kraków" }, new int[] { 0, 1, 5, 0 }, day, 6, 0, routes, train, "Reymont", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa", "Gdańsk" }, new int[] { 0, 2, 5, 0 }, day, 12, 33, routes, express, "Ex Sobieski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa", "Gdańsk" }, new int[] { 0, 2, 5, 0 }, day, 6, 55, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa", "Gdańsk" }, new int[] { 0, 2, 5, 0 }, day, 14, 48, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa" }, new int[] { 0, 2, 0 }, day, 18, 34, routes, express, "Ex Polonia", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa" }, new int[] { 0, 2, 0 }, day, 15, 33, routes, train, "Varsovia", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa" }, new int[] { 0, 2, 0 }, day, 11, 33, routes, train, "Porta Moravica", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa" }, new int[] { 0, 2, 0 }, day, 7, 37, routes, express, "Ex Comenius", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa" }, new int[] { 0, 2, 0 }, day, 4, 31, routes, train, "Chopin", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Częstochowa", "Piotrków Tryb.", "Warszawa" }, new int[] { 0, 5, 1, 0 }, day, 8, 10, routes, train, "Wysocki", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Opole", "Wrocław", "Poznań", "Szczecin" }, new int[] { 0, 2, 15, 5, 0 }, day, 15, 0, routes, train, "Dobrawa", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Opole", "Wrocław", "Poznań", "Szczecin" }, new int[] { 0, 4, 15, 15, 0 }, day, 7, 30, routes, train, "Barnim", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Opole", "Wrocław", "Zielona Góra", "Szczecin" }, new int[] { 0, 4, 15, 15, 0 }, day, 8, 57, routes, train, "Światowid", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Opole", "Wrocław" }, new int[] { 0, 3, 0 }, day, 18, 20, routes, train, "Panorama", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Częstochowa", "Piotrków Tryb.", "Łódź", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 5, 1, 5, 4, 10, 0 }, day, 6, 46, routes, train, "Portowiec", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Kielce", "Radom", "Lublin" }, new int[] { 0, 2, 17, 2, 0 }, day, 6, 45, routes, train, "Godula", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Opole", "Wrocław", "Poznań", "Piła", "Słupsk" }, new int[] { 0, 2, 6, 16, 3, 0 }, day, 5, 5, routes, train, "Gwarek", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Katowice", "Częstochowa", "Piotrków Tryb.", "Łódź", "Kutno", "Toruń", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 2, 1, 2, 2, 2, 10, 0 }, day, 7, 0, routes, train, "Hutnik", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Kielce", "Radom", "Warszawa" }, new int[] { 0, 2, 0 }, day, 5, 24, routes, train, "Nida", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Kraków", "Włoszczowa", "Warszawa", "Białystok", "Suwałki" }, new int[] { 0, 2, 15, 20, 0 }, day, 5, 0, routes, train, "Hańcza", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Kielce", "Włoszczowa", "Warszawa", "Olsztyn" }, new int[] { 0, 3, 2, 15, 0 }, day, 15, 22, routes, train, "Orłowicz", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Kielce", "Włoszczowa", "Warszawa", "Olsztyn" }, new int[] { 0, 3, 2, 15, 0 }, day, 11, 32, routes, train, "Kolberg", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Kielce", "Włoszczowa", "Warszawa", "Olsztyn" }, new int[] { 0, 6, 2, 15, 0 }, day, 7, 20, routes, train, "Żeromski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Włoszczowa", "Warszawa" }, new int[] { 0, 2, 0 }, day, 19, 24, routes, train, "Malinowski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Włoszczowa", "Warszawa" }, new int[] { 0, 2, 0 }, day, 4, 50, routes, train, "Korczak", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa" }, new int[] { 0, 0 }, day, 16, 53, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa" }, new int[] { 0, 0 }, day, 17, 53, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa" }, new int[] { 0, 0 }, day, 19, 59, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa" }, new int[] { 0, 0 }, day, 18, 43, routes, express, "Ex Norwid", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa" }, new int[] { 0, 0 }, day, 14, 38, routes, express, "Ex Sawa", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa" }, new int[] { 0, 0 }, day, 6, 38, routes, express, "Ex Krakus", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa" }, new int[] { 0, 0 }, day, 12, 0, routes, express, "Ex Lwów", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Włoszczowa", "Warszawa", "Siedlce", "Terespol" }, new int[] { 0, 2, 5, 2, 0 }, day, 5, 59, routes, train, "Wit Stwosz", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa", "Gdańsk" }, new int[] { 0, 5, 0 }, day, 15, 53, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa", "Gdańsk", "Słupsk" }, new int[] { 0, 5, 4, 0 }, day, 13, 53, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa", "Gdańsk" }, new int[] { 0, 5, 0 }, day, 5, 53, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa", "Gdańsk" }, new int[] { 0, 5, 0 }, day, 9, 53, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa", "Gdańsk" }, new int[] { 0, 5, 0 }, day, 11, 53, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Włoszczowa", "Warszawa", "Gdańsk" }, new int[] { 0, 3, 25, 0 }, day, 2, 35, routes, train, "Karpaty", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Kielce", "Radom", "Warszawa", "Gdańsk", "Słupsk" }, new int[] { 0, 2, 2, 5, 15, 0 }, day, 19, 13, routes, train, "Ustronie", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Włoszczowa", "Warszawa", "Gdańsk", "Słupsk" }, new int[] { 0, 2, 5, 15, 0 }, day, 9, 24, routes, train, "Pobrzeże", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Kielce", "Radom", "Lublin" }, new int[] { 0, 3, 2, 0 }, day, 5, 19, routes, train, "Jagiełło", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Kielce", "Radom", "Lublin" }, new int[] { 0, 2, 3, 0 }, day, 16, 41, routes, train, "Długosz", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Częstochowa", "Piotrków Tryb.", "Łódź", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 5, 1, 5, 4, 10, 0 }, day, 14, 52, routes, train, "Wawel", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Częstochowa", "Piotrków Tryb.", "Łódź", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 5, 1, 5, 4, 10, 0 }, day, 13, 27, routes, train, "Osterwa", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Częstochowa", "Piotrków Tryb.", "Łódź", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 5, 1, 5, 4, 10, 0 }, day, 6, 52, routes, train, "Barbakan", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Katowice", "Opole", "Wrocław", "Poznań" }, new int[] { 0, 15, 5, 15, 0 }, day, 19, 0, routes, train, "Uznam", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Częstochowa", "Piotrków Tryb.", "Łódź" }, new int[] { 0, 1, 5, 0 }, day, 18, 29, routes, train, "Reymont", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Kraków", "Częstochowa" }, new int[] { 0, 0 }, day, 22, 50, routes, train, "Pogoria", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, day, 6, 35, routes, train, "Wokulski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, day, 10, 15, routes, train, "Prząśniczka", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, day, 13, 20, routes, train, "Tuwim", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, day, 15, 15, routes, train, "Rzecki", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, day, 15, 44, routes, train, "Jagna", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, day, 16, 45, routes, train, "Łęcka", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, day, 17, 5, routes, train, "Łodzianin", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, day, 17, 15, routes, train, "Zosia", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, day, 17, 45, routes, train, "Telimena", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, day, 23, 35, routes, train, "Soplica", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Białystok" }, new int[] { 0, 0 }, day, 15, 10, routes, train, "Branicki", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Poznań", "Zielona Góra" }, new int[] { 0, 4, 4, 0 }, day, 16, 0, routes, express, "Ex Lech", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Poznań", "Zielona Góra" }, new int[] { 0, 5, 10, 0 }, day, 20, 0, routes, train, "Warta", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Olsztyn" }, new int[] { 0, 0 }, day, 5, 55, routes, train, "Warmia", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Gdańsk" }, new int[] { 0, 0 }, day, 7, 55, routes, train, "Rataj", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Kraków" }, new int[] { 0, 3, 0 }, day, 5, 35, routes, train, "Malinowski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Kraków" }, new int[] { 0, 3, 0 }, day, 15, 10, routes, train, "Korczak", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kraków" }, new int[] { 0, 0 }, day, 6, 50, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kraków" }, new int[] { 0, 0 }, day, 7, 50, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kraków" }, new int[] { 0, 0 }, day, 12, 50, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kraków" }, new int[] { 0, 0 }, day, 9, 0, routes, express, "Ex Norwid", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kraków" }, new int[] { 0, 0 }, day, 10, 55, routes, express, "Ex Sawa", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kraków" }, new int[] { 0, 0 }, day, 16, 55, routes, express, "Ex Krakus", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kraków" }, new int[] { 0, 0 }, day, 18, 55, routes, express, "Ex Lwów", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 3, 0 }, day, 6, 55, routes, express, "Ex Polonia", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 3, 0 }, day, 9, 55, routes, train, "Varsovia", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 3, 0 }, day, 13, 55, routes, train, "Porta Moravica", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 3, 5, 0 }, day, 15, 55, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 3, 0 }, day, 17, 54, routes, express, "Ex Comenius", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 3, 0 }, day, 21, 20, routes, train, "Chopin", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Piotrków Tryb.", "Częstochowa", "Katowice" }, new int[] { 0, 1, 6, 0 }, day, 14, 47, routes, train, "Wysocki", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 3, 5, 0 }, day, 17, 58, routes, express, "Ex Chrobry", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 3, 10, 0 }, day, 14, 57, routes, express, "Ex Mewa", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 5, 5, 0 }, day, 6, 59, routes, express, "Ex Podkowiński", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Poznań" }, new int[] { 0, 6, 0 }, day, 19, 0, routes, express, "Ex Krzywousty", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Poznań" }, new int[] { 0, 6, 0 }, day, 9, 59, routes, express, "Ex BWE", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Lublin", "Rzeszów" }, new int[] { 0, 10, 0 }, day, 5, 50, routes, train, "Pomarańczarka", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Piotrków Tryb.", "Częstochowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 1, 5, 15, 0 }, day, 6, 15, routes, train, "Korfanty", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Piotrków Tryb.", "Częstochowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 2, 5, 15, 0 }, day, 16, 14, routes, train, "Górnik", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Częstochowa", "Opole", "Wrocław", "Jelenia Góra" }, new int[] { 0, 4, 3, 10, 0 }, day, 16, 27, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Częstochowa", "Opole", "Wrocław" }, new int[] { 0, 4, 3, 0 }, day, 6, 10, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź", "Wrocław", "Jelenia Góra" }, new int[] { 0, 10, 25, 0 }, day, 5, 5, routes, train, "Asnyk", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Toruń", "Bydgoszcz" }, new int[] { 0, 2, 5, 0 }, day, 14, 20, routes, train, "Ogiński", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Toruń", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 2, 5, 15, 0 }, day, 5, 25, routes, train, "Doker", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Toruń", "Bydgoszcz" }, new int[] { 0, 1, 5, 0 }, day, 16, 31, routes, train, "Kujawiak", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Toruń", "Bydgoszcz" }, new int[] { 0, 1, 5, 0 }, day, 8, 30, routes, train, "Kopernik", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Siedlce", "Terespol" }, new int[] { 0, 1, 0 }, day, 19, 40, routes, train, "Polonez", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Siedlce", "Terespol" }, new int[] { 0, 3, 0 }, day, 18, 10, routes, train, "Kraszewski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Siedlce", "Terespol" }, new int[] { 0, 2, 0 }, day, 16, 12, routes, train, "Niemcewicz", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Siedlce", "Terespol" }, new int[] { 0, 1, 0 }, day, 7, 0, routes, train, "Skaryna", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Radom", "Kielce" }, new int[] { 0, 3, 0 }, day, 19, 18, routes, train, "Nida", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Toruń", "Bydgoszcz", "Piła", "Szczecin" }, new int[] { 0, 2, 2, 5, 17, 0 }, day, 15, 30, routes, train, "Moniuszko", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Toruń", "Bydgoszcz", "Piła" }, new int[] { 0, 2, 5, 19, 0 }, day, 6, 30, routes, train, "Noteć", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Piotrków Tryb.", "Częstochowa", "Opole", "Wrocław" }, new int[] { 0, 2, 2, 2, 0 }, day, 7, 35, routes, express, "Ex Odra", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Warszawa", "Piotrków Tryb.", "Częstochowa", "Opole", "Wrocław" }, new int[] { 0, 2, 2, 2, 0 }, day, 11, 35, routes, train, "Fredro", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Olsztyn", "Gdańsk", "Słupsk", "Szczecin" }, new int[] { 0, 5, 4, 0 }, day, 6, 41, routes, train, "Żuławy", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Olsztyn", "Gdańsk", "Bydgoszcz", "Poznań", "Zielona Góra" }, new int[] { 0, 10, 5, 10, 0 }, day, 9, 25, routes, train, "Ukiel", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Olsztyn", "Gdańsk", "Słupsk", "Szczecin" }, new int[] { 0, 10, 4, 0 }, day, 13, 30, routes, train, "Gryf", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Olsztyn", "Warszawa", "Włoszczowa", "Kielce", "Kraków" }, new int[] { 0, 10, 1, 3, 0 }, day, 5, 18, routes, train, "Orłowicz", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Olsztyn", "Warszawa", "Piotrków Tryb.", "Częstochowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 15, 1, 2, 13, 0 }, day, 7, 30, routes, train, "Kormoran", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Olsztyn", "Warszawa", "Włoszczowa", "Kielce", "Kraków" }, new int[] { 0, 5, 2, 3, 0 }, day, 9, 41, routes, train, "Kolberg", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Olsztyn", "Warszawa", "Włoszczowa", "Kielce", "Kraków" }, new int[] { 0, 5, 2, 3, 0 }, day, 13, 36, routes, train, "Żeromski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Olsztyn", "Warszawa", "Łódź" }, new int[] { 0, 5, 0 }, day, 17, 30, routes, train, "Mazury", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Olsztyn", "Warszawa" }, new int[] { 0, 0 }, day, 19, 30, routes, train, "Warmia", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Piła", "Bydgoszcz", "Toruń", "Kutno", "Warszawa" }, new int[] { 0, 17, 2, 1, 0 }, day, 16, 45, routes, train, "Noteć", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Poznań", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 15, 0 }, day, 6, 31, routes, train, "Bałtyk", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Poznań", "Kutno", "Warszawa" }, new int[] { 0, 3, 0 }, day, 6, 24, routes, express, "Ex Krzywousty", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Poznań", "Kutno", "Warszawa" }, new int[] { 0, 3, 0 }, day, 15, 26, routes, express, "Ex BWE", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Poznań", "Kutno", "Warszawa", "Lublin", "Rzeszów", "Przemyśl" }, new int[] { 0, 4, 10, 25, 15, 0 }, day, 9, 30, routes, train, "Gombrowicz", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Poznań", "Wrocław", "Opole", "Katowice", "Kraków", "Rzeszów", "Przemyśl" }, new int[] { 0, 10, 4, 15, 5, 25, 0 }, day, 6, 0, routes, train, "Siemiradzki", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Poznań", "Wrocław", "Opole", "Katowice", "Kraków" }, new int[] { 0, 10, 6, 15, 0 }, day, 2, 0, routes, train, "Uznam", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Lublin", "Warszawa", "Kutno", "Toruń", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 20, 24, 15, 1, 2, 15, 0 }, day, 3, 33, routes, train, "Kochanowski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Kraków", "Częstochowa", "Opole", "Wrocław", "Poznań", "Szczecin" }, new int[] { 0, 8, 15, 15, 3, 5, 10, 0 }, day, 6, 27, routes, train, "Matejko", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Kraków", "Częstochowa", "Opole", "Wrocław", "Poznań", "Szczecin" }, new int[] { 0, 3, 35, 2, 5, 5, 15, 0 }, day, 4, 31, routes, train, "Chełmoński", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Kraków", "Katowice", "Opole", "Wrocław", "Poznań", "Szczecin" }, new int[] { 0, 10, 15, 15, 3, 5, 10, 0 }, day, 18, 26, routes, train, "Przemyślanin", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Lublin", "Warszawa", "Kutno", "Poznań" }, new int[] { 0, 33, 25, 15, 5, 0 }, day, 7, 27, routes, train, "Gombrowicz", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Kraków", "Katowice", "Opole", "Wrocław", "Poznań" }, new int[] { 0, 25, 15, 15, 5, 10, 0 }, day, 12, 26, routes, train, "Siemiradzki", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Kraków", "Katowice", "Opole", "Wrocław", "Jelenia Góra" }, new int[] { 0, 20, 10, 5, 2, 15, 0 }, day, 11, 16, routes, train, "Mehoffer", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Kraków", "Katowice", "Opole", "Wrocław" }, new int[] { 0, 20, 15, 15, 3, 0 }, day, 14, 29, routes, train, "Wyspiański", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Kraków", "Częstochowa", "Opole", "Wrocław", "Poznań", "Piła", "Słupsk" }, new int[] { 0, 2, 5, 3, 2, 12, 14, 1, 0 }, day, 8, 49, routes, train, "Malczewski", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Rzeszów", "Lublin", "Warszawa" }, new int[] { 0, 15, 0 }, day, 16, 55, routes, train, "Pomarańczarka", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Rzeszów", "Kraków", "Warszawa", "Gdańsk" }, new int[] { 0, 15, 5, 0 }, day, 6, 0, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Rzeszów", "Lublin", "Warszawa", "Kutno", "Toruń", "Bydgoszcz" }, new int[] { 0, 17, 5, 1, 4, 0 }, day, 12, 20, routes, train, "Staszic", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Słupsk", "Gdańsk", "Warszawa", "Kraków" }, new int[] { 0, 3, 10, 0 }, day, 7, 54, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Słupsk", "Gdańsk", "Warszawa", "Radom", "Kielce", "Kraków" }, new int[] { 0, 5, 5, 1, 2, 0 }, day, 22, 25, routes, train, "Ustronie", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Słupsk", "Gdańsk", "Warszawa", "Włoszczowa", "Kraków" }, new int[] { 0, 5, 5, 2, 0 }, day, 8, 57, routes, train, "Pobrzeże", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Słupsk", "Piła", "Poznań", "Wrocław", "Opole", "Częstochowa", "Kraków", "Rzeszów", "Przemyśl" }, new int[] { 0, 1, 19, 6, 2, 1, 7, 2, 0 }, day, 5, 22, routes, train, "Malczewski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Słupsk", "Piła", "Poznań", "Wrocław", "Opole", "Katowice" }, new int[] { 0, 2, 17, 5, 3, 0 }, day, 13, 26, routes, train, "Gwarek", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Suwałki", "Białystok", "Warszawa", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 25, 10, 2, 5, 0 }, day, 6, 46, routes, train, "Podlasiak", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Suwałki", "Białystok", "Warszawa", "Włoszczowa", "Kraków" }, new int[] { 0, 20, 10, 2, 0 }, day, 15, 37, routes, train, "Hańcza", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Warszawa", "Białystok", "Suwałki" }, new int[] { 0, 15, 3, 15, 20, 0 }, day, 10, 41, routes, train, "Podlasiak", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Słupsk", "Gdańsk", "Olsztyn", "Białystok" }, new int[] { 0, 3, 15, 20, 0 }, day, 11, 9, routes, train, "Rybak", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Słupsk", "Gdańsk", "Olsztyn" }, new int[] { 0, 4, 15, 0 }, day, 14, 20, routes, train, "Żuławy", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Słupsk", "Gdańsk", "Olsztyn" }, new int[] { 0, 5, 5, 0 }, day, 7, 18, routes, train, "Gryf", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Słupsk", "Gdańsk" }, new int[] { 0, 6, 0 }, day, 17, 17, routes, train, "Albatros", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Wrocław", "Opole", "Częstochowa", "Kraków", "Rzeszów", "Przemyśl" }, new int[] { 0, 5, 10, 2, 15, 20, 11, 0 }, day, 9, 52, routes, train, "Matejko", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Wrocław", "Opole", "Częstochowa", "Kraków", "Rzeszów", "Przemyśl" }, new int[] { 0, 5, 10, 2, 15, 20, 11, 0 }, day, 11, 49, routes, train, "Chełmoński", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Wrocław", "Opole", "Katowice", "Kraków", "Rzeszów", "Przemyśl" }, new int[] { 0, 5, 10, 2, 15, 20, 10, 0 }, day, 19, 35, routes, train, "Przemyślanin", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Wrocław", "Opole", "Katowice" }, new int[] { 0, 10, 15, 3, 0 }, day, 5, 44, routes, train, "Dobrawa", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Wrocław", "Opole", "Katowice" }, new int[] { 0, 5, 15, 4, 0 }, day, 13, 50, routes, train, "Barnim", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Zielona Góra", "Wrocław", "Opole", "Katowice" }, new int[] { 0, 5, 15, 4, 0 }, day, 11, 35, routes, train, "Światowid", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Warszawa", "Lublin" }, new int[] { 0, 10, 4, 15, 0 }, day, 14, 43, routes, train, "Gałczyński", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Zielona Góra", "Wrocław" }, new int[] { 0, 15, 0 }, day, 7, 6, routes, train, "Swarożyc", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Warszawa" }, new int[] { 0, 10, 3, 0 }, day, 5, 56, routes, express, "Ex Chrobry", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Warszawa" }, new int[] { 0, 5, 3, 0 }, day, 7, 53, routes, express, "Ex Mewa", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Warszawa" }, new int[] { 0, 10, 4, 0 }, day, 15, 59, routes, express, "Ex Podkowiński", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Łódź", "Piotrków Tryb.", "Częstochowa", "Kraków" }, new int[] { 0, 15, 3, 15, 2, 2, 0 }, day, 4, 50, routes, train, "Wawel", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Łódź", "Piotrków Tryb.", "Częstochowa", "Kraków" }, new int[] { 0, 15, 3, 15, 2, 2, 0 }, day, 6, 58, routes, train, "Osterwa", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Łódź", "Piotrków Tryb.", "Częstochowa", "Katowice" }, new int[] { 0, 15, 3, 15, 2, 2, 0 }, day, 8, 49, routes, train, "Portowiec", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Łódź", "Piotrków Tryb.", "Częstochowa", "Kraków" }, new int[] { 0, 15, 3, 15, 2, 2, 0 }, day, 12, 54, routes, train, "Barbakan", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Łódź" }, new int[] { 0, 15, 3, 0 }, day, 16, 54, routes, train, "Włókniarz", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Szczecin", "Piła", "Bydgoszcz", "Toruń", "Kutno", "Warszawa" }, new int[] { 0, 20, 5, 2, 2, 0 }, day, 5, 49, routes, train, "Moniuszko", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Terespol", "Siedlce", "Warszawa", "Włoszczowa", "Kraków" }, new int[] { 0, 3, 5, 2, 0 }, day, 16, 23, routes, train, "Wit Stwosz", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Terespol", "Siedlce", "Warszawa" }, new int[] { 0, 1, 0 }, day, 5, 18, routes, train, "Polonez", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Terespol", "Siedlce", "Warszawa" }, new int[] { 0, 3, 0 }, day, 7, 23, routes, train, "Kraszewski", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Terespol", "Siedlce", "Warszawa" }, new int[] { 0, 2, 0 }, day, 10, 38, routes, train, "Niemcewicz", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Terespol", "Siedlce", "Warszawa" }, new int[] { 0, 1, 0 }, day, 19, 18, routes, train, "Skaryna", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Wrocław", "Łódź", "Warszawa", "Białystok" }, new int[] { 0, 5, 15, 0 }, day, 7, 0, routes, train, "Konopnicka", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Wrocław", "Poznań", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 7, 5, 0 }, day, 16, 30, routes, train, "Piast", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Wrocław", "Poznań", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 17, 3, 0 }, day, 13, 27, routes, train, "Pomorzanin", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Wrocław", "Poznań", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 14, 6, 0 }, day, 6, 15, routes, train, "Mieszko", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Częstochowa", "Warszawa", "Gdańsk" }, new int[] { 0, 1, 2, 5, 0 }, day, 14, 16, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Wrocław", "Zielona Góra", "Szczecin" }, new int[] { 0, 15, 0 }, day, 15, 46, routes, train, "Swarożyc", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Wrocław", "Łódź", "Warszawa", "Lublin" }, new int[] { 0, 10, 10, 0 }, day, 11, 0, routes, train, "Słowacki", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Częstochowa", "Warszawa" }, new int[] { 0, 4, 5, 0 }, day, 5, 3, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Katowice" }, new int[] { 0, 6, 0 }, day, 7, 55, routes, train, "Panorama", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Katowice", "Kraków", "Rzeszów", "Przemyśl" }, new int[] { 0, 2, 15, 15, 23, 0 }, day, 6, 16, routes, train, "Wyspiański", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Częstochowa", "Warszawa", "Gdańsk" }, new int[] { 0, 2, 5, 10, 0 }, day, 10, 17, routes, express, "Ex Premium", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Katowice", "Włoszczowa", "Kielce", "Radom", "Lublin" }, new int[] { 0, 2, 13, 2, 10, 3, 0 }, day, 12, 52, routes, train, "Bolko", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Katowice", "Włoszczowa", "Kielce", "Radom", "Lublin" }, new int[] { 0, 2, 11, 3, 12, 2, 0 }, day, 8, 40, routes, train, "Sztygar", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Częstochowa", "Piotrków Tryb.", "Warszawa" }, new int[] { 0, 2, 2, 2, 0 }, day, 18, 33, routes, express, "Ex Odra", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Częstochowa", "Piotrków Tryb.", "Warszawa" }, new int[] { 0, 2, 2, 2, 0 }, day, 19, 6, routes, train, "Fredro", seats, expressSeats, rand));

            connections.Add(CreateConnection(new string[] { "Zielona Góra", "Poznań", "Kutno", "Warszawa" }, new int[] { 0, 6, 3, 0 }, day, 5, 52, routes, express, "Ex Lech", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Zielona Góra", "Poznań", "Bydgoszcz", "Gdańsk", "Olsztyn" }, new int[] { 0, 15, 10, 15, 0 }, day, 12, 55, routes, train, "Ukiel", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Zielona Góra", "Poznań", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 15, 15, 0 }, day, 8, 46, routes, train, "Bachus", seats, expressSeats, rand));
            connections.Add(CreateConnection(new string[] { "Zielona Góra", "Poznań", "Kutno", "Warszawa" }, new int[] { 0, 17, 6, 0 }, day, 2, 53, routes, train, "Warta", seats, expressSeats, rand));

            return connections;
        }

        private static Connection CreateConnection(string[] stations, int[] offsets, DateTime datetime, int hour, int minutes, List<Route> routes, Train train, string name, string seats, string expressSeats, Random rand)
        {
            ConnectionPart[] conParts = new ConnectionPart[stations.Length - 1];
            int offset = 0;
            string seat = seats;
            int numberOfSeats = 80;
            if (train.Type == "Ekspres")
            {
                seat = expressSeats;
                numberOfSeats = 60;
            }
            for (int i = 0; i < conParts.Length; i++)
            {
                var query = routes.Where(r => r.From == stations[i] && r.To == stations[i + 1]).First();
                int reserveTime = rand.Next(query.BestTime / 10);
                if (train.Type == "Ekspres")
                {
                    reserveTime = -reserveTime;
                }
                conParts[i] = new ConnectionPart()
                {
                    StartTime = new DateTime(datetime.Year, datetime.Month, datetime.Day, hour, minutes, 0).AddMinutes(offset + offsets[i]),
                    EndTime = new DateTime(datetime.Year, datetime.Month, datetime.Day, hour, minutes, 0).AddMinutes(offset + offsets[i] + query.BestTime + reserveTime),
                    Route = query,
                    FreeSeats = numberOfSeats,
                    Seats = seat
                };
                offset += offsets[i];
                offset += query.BestTime + reserveTime;
            }
            Connection con = new Connection()
            {
                StartPlace = stations[0],
                EndPlace = stations[stations.Length - 1],
                Train = train,
                Name = name,
                Parts = conParts.ToList()
            };
            return con;
        }
    }
}
