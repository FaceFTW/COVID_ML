//data filtering

using System.IO;

var rows = File.ReadLines("COVID-DATA.csv");
int currentRow = 0;
int validCases = 0;
int filteredCases_missinginfo = 0;
int filteredCases_probableStatus = 0;

//Write to file
using (var writer = File.CreateText("filtered_data_arules.csv")) {
	//Will be parsed into a sparse matrix in R
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

		//Comment out if not using regression trees
		if(date == "NA"){
			filteredCases_missinginfo++;
			currentRow++;
			continue;
		}
		DateTime dateObj = DateTime.Parse(date);
		//Figure out which quarter the date is in
		var quarter = (dateObj.Month - 1) / 3 + 1;
		var quarterString = "Q" + quarter.ToString()+ " " + dateObj.Year.ToString();
		if (dateObj <= DateTime.Parse("2020-01-01")) {
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

		var exposure_yn = columns[12] switch {
			"Yes" => "Had Exposure",
			"No" => "No Exposure",
			_ => "SKIP"
		};
		if (exposure_yn == "SKIP") {
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

		var hosp_yn =  columns[15] switch {
			"Yes"=> "Hospitalized",
			"No" => "Not Hospitalized",
			_ => "SKIP"
		};
		if (hosp_yn == "SKIP") {
				filteredCases_missinginfo++;
				currentRow++;
				continue;
		}

		var icu_yn = columns[16] switch {
			"Yes" => "ICU Case",
			"No" => "Not ICU Case",
			_ => "SKIP"
		};
		if (icu_yn == "SKIP") {
				filteredCases_missinginfo++;
				currentRow++;
				continue;
		}


		var death_yn = columns[17] switch {
 			"Yes"=> "Death",
			"No" => "Survived",
			_ => "SKIP"
		};
		if (death_yn == "SKIP") {
				filteredCases_missinginfo++;
				currentRow++;
				continue;
		}

		var underlying_conditions_yn = columns[18] switch {
			"Yes" => "Has Underlying Conditions",
			"No" => "No Underlying Conditions",
			_ => "SKIP"
		};
		if (underlying_conditions_yn == "SKIP") {
				filteredCases_missinginfo++;
				currentRow++;
				continue;
		}

		//Regenerate Row
		var rowData = new string[]{
			quarterString,
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