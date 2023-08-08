using System.IO;

var rows = File.ReadLines("../COVID-DATA.csv");
int currentRow = 0;
int validCases = 0;
int filteredCases_missinginfo = 0;
int filteredCases_probableStatus = 0;

//Clean the target file if it exists
if (File.Exists("filtered_data.csv")) {
	File.Delete("filtered_data.csv");
}
File.CreateText("filtered_data.csv");

//Write to file
using (var writer = new StreamWriter(File.OpenWrite("filtered_data.csv"))) {
	//CSV Header
	writer.WriteLine("state,age_group,sex,exposure_yn,symptom_status,hosp_yn,icu_yn,underlying_conditions_yn,death_yn");

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

		//var date = columns[0];
		var state = columns[1];
		var age_group = columns[5];
		var sex = columns[6];

		var exposure_yn = columns[1];
		if (exposure_yn == "Missing") {
			filteredCases_missinginfo++;
			currentRow++;
			continue;
		}

		var symptom_status = columns[14];
		if (symptom_status == "Missing" || symptom_status == "Unknown") {
			filteredCases_missinginfo++;
			currentRow++;
			continue;
		}

		var hosp_yn = columns[15];
		if (hosp_yn == "Missing" || hosp_yn == "Unknown") {
			filteredCases_missinginfo++;
			currentRow++;
			continue;
		}

		var icu_yn = columns[16];
		if (icu_yn == "Missing" || icu_yn == "Unknown") {
			filteredCases_missinginfo++;
			currentRow++;
			continue;
		}

		var death_yn = columns[17];
		if (death_yn == "Missing" || death_yn == "Unknown" || death_yn == "NA") {
			filteredCases_missinginfo++;
			currentRow++;
			continue;
		}

		var underlying_conditions_yn = columns[18];
		if (underlying_conditions_yn == null) {
			filteredCases_missinginfo++;
			currentRow++;
			continue;
		}

		//Regenerate Row
		var rowData = new string[]{
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
}
Console.WriteLine("Total rows: " + currentRow);
Console.WriteLine("Valid cases: " + validCases);
Console.WriteLine("Filtered cases (missing info): " + filteredCases_missinginfo);
Console.WriteLine("Filtered cases (probable status): " + filteredCases_probableStatus);
Console.WriteLine("Filtered cases (total): " + (filteredCases_missinginfo + filteredCases_probableStatus));