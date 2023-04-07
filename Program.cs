namespace Assessment_3
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Menu
			Console.WriteLine("Welcome to College Sports Program");
			CollegeSportsProgram collegeSportsProgram = new CollegeSportsProgram();
			collegeSportsProgram.MainFunction();
		}
	}

	class CollegeSportsProgram
	{
		private List<string> sports_list = new List<string>();
		private List<string> tournamnet_list = new List<string> ();
		private List<string> completed_tournament_list = new List<string>();


		private static string sports_file_path = "C:\\Users\\HP\\Documents\\Projects\\Razor Pages\\Assessment 3\\DATA\\sports.txt";
		private static string tournament_file_path = "C:\\Users\\HP\\Documents\\Projects\\Razor Pages\\Assessment 3\\DATA\\tournaments.txt";
		private static string scorecard_file_path = "C:\\Users\\HP\\Documents\\Projects\\Razor Pages\\Assessment 3\\DATA\\scorecard.txt";


		private StreamReader sportsReader = new StreamReader(sports_file_path);
		//private StreamWriter sportsWriter = new StreamWriter(sports_file_path, false);

		private StreamReader tournamentReader = new StreamReader(tournament_file_path);
		//private StreamWriter tournamentWriter = new StreamWriter(tournament_file_path, false);

		//private StreamReader scorecardReader = new StreamReader(scorecard_file_path);
		//private StreamWriter scorecardWriter = new StreamWriter(scorecard_file_path, append: true);

		public CollegeSportsProgram()
		{
			GetSportsList();
			GetTournamnetList();
		}



		public void MainFunction()
		{
			string menu = "1. Add Sports\n2. Add Scoreboard\n3. Add Tournament\n4. Remove Sports\n5. Edit Scoreboard\n6. Exit";
			Console.WriteLine(menu);
			Console.Write("Choose the option for the operation : ");

			int choice = int.Parse(Console.ReadLine());
			switch(choice)
			{
				case 1:
					AddSports();
					break;
				case 2:
					AddScoreCard();
					break;
				case 3:
					AddTournament();
					break;
				case 4:
					RemoveSport();
					break;
				case 5:
					EditScoreCard();
					break;
				case 6:
					ExitApp();
					break;
				default:
					Console.WriteLine("Entered Wrong Option");
					break;
			}
			return;

		}

		private void GetSportsList()
		{
			string result = sportsReader.ReadToEnd();
			if(result !=  null)
			{
				var temp_storage = result.Split(Environment.NewLine).ToList<string>();
				foreach( var temp in temp_storage)
				{
					if(!temp.Equals(" "))
						sports_list.Add(temp);
				}
			}
		}
		private void GetTournamnetList()
		{
			string result = tournamentReader.ReadToEnd();
            if (result != null)
            {
                var temp_storage = result.Split(Environment.NewLine).ToList<string>();
                foreach (var temp in temp_storage)
                {
                    if (!temp.Equals(" "))
                        tournamnet_list.Add(temp);
                }
            }
        }


		public void AddSports()
		{
			Console.Write("Enter the name of the sport : ");
			string newSport = Console.ReadLine();
			if (sports_list.Contains(newSport))
			{
				Console.WriteLine("The Sport already existed!!");
				MainFunction();
				return;
			}
			else
			{
				//sportsWriter.Write("\n" + newSport);
				sports_list.Add(newSport);

			}
			Console.WriteLine("The sport added successfully!!");

			MainFunction();
			return;

		}

		public void RemoveSport()
		{
			Console.Write("Enter the name of the Sport : ");
			string removeSport = Console.ReadLine() as string;
			if(sports_list.Contains(removeSport))
			{
				sports_list.Remove(removeSport);
				Console.WriteLine("The sport removed successfully");

				MainFunction();
				return;
			}
			else
			{
				Console.WriteLine("The Entered Not Exists!!!");
				MainFunction();
				return;
			}
		}

		// NEED TO GET PLAYER COUNT FOR EACH Tournament
		public void AddTournament()
		{
			Console.Write("The List of Sports avaliable : ");
			foreach(string sport in sports_list)
			{
				Console.Write(sport + " ");
			}
			Console.WriteLine();
			Console.WriteLine("Enter the name of the tournament : ");
			string newTournament = Console.ReadLine();
			//Console.WriteLine("Enter the Type of Tournament: \n1. Single\n2. Group");
			//int touranamentType = int.Parse(Console.ReadLine());
			//int team_capacity = 1;
			//if(touranamentType == 2)
			//{
			//	Console.WriteLine("Enter the team capacity allowed : ");
			//	team_capacity = int.Parse(Console.ReadLine());
			//}

			if(tournamnet_list.Contains(newTournament))
			{
				Console.WriteLine("The tournament already existed");
			}
			//else if (!sports_list.Contains(newTournament.ToLower()))
			//{
			//	Console.WriteLine("The Entered sport to NOT EXIST!");
			//}
			else
			{
				
				tournamnet_list.Add(newTournament);
			}

			Console.WriteLine("The Tournament added successfully");
			MainFunction();
			return;

		}


		public void AddScoreCard()
		{
			Console.WriteLine("Enter the Tournament Name : ");
			string sport = Console.ReadLine().ToLower();

			Console.WriteLine("Enter Team 1 name : ");
			string team1 = Console.ReadLine().ToLower();

			Console.WriteLine("Enter Team 1 score : ");
			int score1 = int.Parse(Console.ReadLine());

			Console.WriteLine("Enter Team 2 name : ");
			string team2 = Console.ReadLine().ToLower();

			Console.WriteLine("Enter Team 2 score : ");
			int score2 = int.Parse(Console.ReadLine());

			Console.WriteLine("Enter Tournamnet Status :\n1. Not Over\n2. Over");
			bool tournament_status = int.Parse(Console.ReadLine()) == 1 ? false : true;

			// Need to call removetournament and remove players if the tournamnet is over (true);

			if (tournament_status)
			{
				RemoveTournament(sport);
			}

			using (StreamWriter scorecardWriter = File.AppendText(scorecard_file_path))
			{
				scorecardWriter.WriteLine(sport + "," + team1 + "," + score1 + "," + team2 + "," + score2 + "," + tournament_status);
			}

			//scorecardWriter.WriteLine(sport + "," + team1 + "," + score1 + "," + team2 + "," + score2 + "," + tournament_status);

			Console.WriteLine("ScoreCard is Added Successfully");
			MainFunction();
			return;

		}

		public void EditScoreCard()
		{
			Console.WriteLine("Avaliable ScoreBoards : ");
			using (StreamReader sr = new StreamReader(scorecard_file_path))
			{
				string line;
				int i = 1;
				while ((line = sr.ReadLine()) != null)
				{
					if(line.Split(',')[0].Length > 1)
					{
						Console.WriteLine(i + ". " + line.Split(',')[0]);
						i++;
					}
				}
			}
			Console.Write("Enter the number of the scorecard to edit : ");
			int scorecardNo = int.Parse(Console.ReadLine());

			string[] scoreDetails = File.ReadAllLines(scorecard_file_path);

			Console.WriteLine("Tournament details: " + scoreDetails[scorecardNo - 1].Split(',')[0]);
			Console.WriteLine("Team 1: " + scoreDetails[scorecardNo - 1].Split(',')[1]);
			Console.WriteLine("Score 1: " + scoreDetails[scorecardNo - 1].Split(',')[2]);
			Console.WriteLine("Team 2: " + scoreDetails[scorecardNo - 1].Split(',')[3]);
			Console.WriteLine("Score 2: " + scoreDetails[scorecardNo - 1].Split(',')[4]);
			string tournament_status_msg = scoreDetails[scorecardNo - 1].Split(',')[5] == "False" ? "Not Over" : "Over";

			Console.WriteLine("Tournament Status : " + tournament_status_msg);

			if(tournament_status_msg == "Over")
			{
				Console.WriteLine("The Tournament is already Over!!");
				MainFunction();
				return;
			}

			Console.WriteLine("Enter updated team 1 score:");
			int score1 = int.Parse(Console.ReadLine());

			Console.WriteLine("Enter updated team 2 score:");
			int score2 = int.Parse(Console.ReadLine());

			Console.WriteLine("Enter Tournamnet Status :\n1. Not Over\n2. Over");
			bool tournament_status = (int.Parse(Console.ReadLine()) == 1 )? false : true;

			scoreDetails[scorecardNo - 1] = scoreDetails[scorecardNo - 1].Split(',')[0] + "," + scoreDetails[scorecardNo - 1].Split(',')[1] + "," + score1 + "," + scoreDetails[scorecardNo - 1].Split(',')[3] + "," + score2 + ',' + tournament_status;

			if(tournament_status == true)
			{
				RemoveTournament(scoreDetails[scorecardNo - 1].Split(',')[0]);
			}

			File.WriteAllLines(scorecard_file_path, scoreDetails);

			Console.WriteLine("Scoreboard updated successfully.");
			MainFunction();


		}


		public void RemoveTournament(string tournamentName)
		{

			tournamentName = tournamentName.Trim();
			tournamnet_list.Remove(tournamentName);
			return;
		}


		public void ExitApp()
		{
			sportsReader.Close();
			tournamentReader.Close();
			//FileStream fsOverwrite = new FileStream(sports_file_path, FileMode.Create);
			//StreamWriter swOverwrite = new StreamWriter(fsOverwrite);

			//foreach (var sport in sports_list)
			//	{
			//		Console.WriteLine("here From exit");
			//		Console.WriteLine(sport);
			//		swOverwrite.WriteLine(sport);
			//	}
			using (StreamWriter sportWriter = new StreamWriter(sports_file_path, false))
			{
				foreach (var sport in sports_list)
				{
					if (!sport.Equals(" "))
						sportWriter.WriteLine(sport);
				}
			}
			using (StreamWriter tournamentWriter = new StreamWriter(tournament_file_path, false))
			{
				foreach (var tournament in tournamnet_list)
				{
					if(!tournament.Equals(" "))
						tournamentWriter.WriteLine(tournament);
				}
			}

			return;

		}



	}



}