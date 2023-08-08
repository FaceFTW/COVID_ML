//data filtering

using System.IO;

var rows = File.ReadLines("weekly_covid_deaths.csv");
int currentRow = 0;
int validCases = 0;
int filteredCases_missinginfo = 0;
int filteredCases_probableStatus = 0;

//Write to file
using (var writer = File.CreateText("weekly_deaths_converted_data.csv")) {
	//CSV Header
	writer.WriteLine("state,week,new_cases,total_cases,new_deaths,total_deaths");

	foreach (var row in rows) {
		if (currentRow == 0) {
			currentRow++;
			continue;
		};

		var columns = row.Split(',');

		var state = columns[1];
		var date= columns[3];	//use end date as point of reference (deaths actually happened then)
		DateTime dateObj = DateTime.Parse(date);
		//Get difference from 1/1/2020 in weeks
		var weeksSince = (dateObj - new DateTime(2020, 1, 1)).TotalDays / 7;

		var total_cases = columns[4];
		var new_cases = columns[5];
		var total_deaths = columns[6];
		var new_deaths = columns[7];
		// var historic_deaths = columns[8];
		// var new_historic_deaths = columns[9];

		//Regenerate Row
		var rowData = new string[]{
			state,
			weeksSince.ToString(),
			new_cases,
			total_cases,
			new_deaths,
			total_deaths
		};
		var newCsvRow = string.Join(",", rowData);
		writer.WriteLine(newCsvRow);

		//Stat tracking for console
		currentRow++;
		validCases++;
		if (currentRow % 1000000 == 0) {
			Console.WriteLine("Processed " + currentRow + " rows");
		}
	}
	writer.Close();

}
Console.WriteLine("Total rows: " + currentRow);
Console.WriteLine("Valid cases: " + validCases);
Console.WriteLine("Filtered cases (missing info): " + filteredCases_missinginfo);
Console.WriteLine("Filtered cases (probable status): " + filteredCases_probableStatus);
Console.WriteLine("Filtered cases (total): " + (filteredCases_missinginfo + filteredCases_probableStatus));