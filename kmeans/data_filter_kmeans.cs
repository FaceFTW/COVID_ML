//data filtering

using System.IO;

var rows = File.ReadLines("COVID-19_Case_Surveillance_Public_Use_Data_with_Geography.csv");
int currentRow = 0;
int validCases = 0;
int filteredCases_missinginfo = 0;
int filteredCases_probableStatus = 0;

//Write to file
using (var writer = File.CreateText("filtered_data_kmeans.csv")) {

	//CSV Header
	writer.WriteLine("weeks_since,state,age_group,sex,exposure_yn,symptom_status,hosp_yn,icu_yn,underlying_conditions_yn,death_yn");

	foreach (var row in rows) {
		if (currentRow == 0) {
			currentRow++;
			continue;
		};

		var columns = row.Split(',');

		//Check if status is probable
		if (columns[13] != "Laboratory-confirmed case") {
			filteredCases_probableStatus++;
			currentRow++;
			continue;
		}

		var date = columns[0];
		if(date == "NA"){
			filteredCases_missinginfo++;
			currentRow++;
			continue;
		}
		DateTime dateObj = DateTime.Parse(date);
		//Get difference from 1/1/2020
		var weeks_since = Math.Round((dateObj - new DateTime(2020, 1, 1)).TotalDays / 7);
		if (weeks_since < 0) {
			filteredCases_missinginfo++;
			currentRow++;
			continue;
		}


		var state = columns[1];
		if(state == "NA"){
			filteredCases_missinginfo++;
			currentRow++;
			continue;
		}
		var age_group = columns[5];
		if(age_group == "NA"){
			filteredCases_missinginfo++;
			currentRow++;
			continue;
		}
		var sex = columns[6];
		if(sex == "NA"){
			filteredCases_missinginfo++;
			currentRow++;
			continue;
		}

		var exposure_yn = columns[12];
		if (exposure_yn != "Yes" && exposure_yn != "Unknown") {
				filteredCases_missinginfo++;
				currentRow++;
				continue;
		};

		var symptom_status = columns[14];
		if (symptom_status == "Missing" || symptom_status == "Unknown") {
			filteredCases_missinginfo++;
			currentRow++;
			continue;
		}

		var hosp_yn =  columns[15];
		if (hosp_yn != "Yes" && hosp_yn != "No") {
				filteredCases_missinginfo++;
				currentRow++;
				continue;
		}

		var icu_yn = columns[16];
		if (icu_yn != "Yes" && icu_yn != "No") {
				filteredCases_missinginfo++;
				currentRow++;
				continue;
		}


		var death_yn = columns[17];
		if (death_yn != "Yes" && death_yn != "No") {
				filteredCases_missinginfo++;
				currentRow++;
				continue;
		}

		var underlying_conditions_yn = columns[18] ;
		if (underlying_conditions_yn != "Yes" && underlying_conditions_yn != "No") {
				filteredCases_missinginfo++;
				currentRow++;
				continue;
		}

		//Regenerate Row
		var rowData = new string[]{
			weeks_since.ToString(),
			state,
			age_group,
			sex,
			exposure_yn,
			symptom_status,
			hosp_yn,
			icu_yn,
			underlying_conditions_yn,
			death_yn
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