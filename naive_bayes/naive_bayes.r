
#########################################
# Data Import + Prep
#########################################
covid_data <- read.csv("filtered_data.csv")
# covid_data <- covid_data[-1] # remove first column
covid_data$state <- as.factor(covid_data$state)
covid_data$age_group <- as.factor(covid_data$age_group)
covid_data$sex <- as.factor(covid_data$sex)
covid_data$exposure_yn <- as.factor(covid_data$exposure_yn)
covid_data$symptom_status <- as.factor(covid_data$symptom_status)
covid_data$hosp_yn <- as.factor(covid_data$hosp_yn)
covid_data$icu_yn <- as.factor(covid_data$icu_yn)
covid_data$underlying_conditions_yn <- as.factor(covid_data$underlying_conditions_yn)
covid_data$death_yn <- as.factor(covid_data$death_yn)

# check for blanks
if(sum(is.na(covid_data)) > 0) {
	stop("There are blanks in the data")
} else {
  print("There are no blanks in the data. Showing Preview:")
  str(covid_data)
}

#########################################
# Separating Training and Testing Data
#########################################
training_count <- 300000
testing_count <- 59307

covid_data_train <- covid_data[1:training_count,1:8]
covid_data_test <- covid_data[(training_count+1):(training_count+testing_count),1:8]

covid_data_train_label <- covid_data[1:training_count,9]
covid_data_test_label <- covid_data[(training_count + 1):(training_count + testing_count),9]

# check proportions
print("Proportions of Datasets:")
prop.table(table(covid_data_train_label))
prop.table(table(covid_data_test_label))

########################
# Naive Bayes Model Below
########################
library(gmodels)
library(e1071)

model <- naiveBayes(covid_data_train, covid_data_train_label)
predictions <- predict(model, covid_data_test, type = "class")

CrossTable(x=predictions, y=covid_data_test_label, prop.chisq = FALSE, prop.c = FALSE, prop.r = FALSE, dnn = c('predicted', 'actual'))
