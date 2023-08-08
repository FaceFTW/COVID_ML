# Data Prep
covid_data <- read.csv("filtered_data.csv")

covid_data$state <- as.factor(covid_data$state)
covid_data$age_group <- as.factor(covid_data$age_group)
covid_data$sex <- as.factor(covid_data$sex)
covid_data$exposure_yn <- as.factor(covid_data$exposure_yn)
covid_data$symptom_status <- as.factor(covid_data$symptom_status)
covid_data$hosp_yn <- as.factor(covid_data$hosp_yn)
covid_data$icu_yn <- as.factor(covid_data$icu_yn)
covid_data$underlying_conditions_yn <- as.factor(covid_data$underlying_conditions_yn)
covid_data$death_yn <- as.factor(covid_data$death_yn)

#check for blanks
sum(is.na(covid_data))

covid_data <- covid_data[-1]

covid_data$age_group <- as.numeric(as.integer(covid_data$age_group))
covid_data$sex <- as.numeric(as.integer(covid_data$sex))
covid_data$exposure_yn <- as.numeric(as.integer(covid_data$exposure_yn))
covid_data$symptom_status <- as.numeric(as.integer(covid_data$symptom_status))
covid_data$hosp_yn <- as.numeric(as.integer(covid_data$hosp_yn))
covid_data$icu_yn <- as.numeric(as.integer(covid_data$icu_yn))
covid_data$underlying_conditions_yn <- as.numeric(as.integer(covid_data$underlying_conditions_yn))

str(covid_data)

# Modeling/Analysis

library(class)
library(gmodels)

#use a subset of the data for training and testing
training_count <- 300000
testing_count <- 50000


covid_data_train <- covid_data[1:training_count,1:7]
covid_data_test <- covid_data[(training_count+1):(training_count+testing_count),1:7]

covid_data_train_label <- covid_data[1:training_count,8]
covid_data_test_label <- covid_data[(training_count + 1):(training_count + testing_count),8]

covid_data_death_pred <- knn(train = covid_data_train, test = covid_data_test, cl = covid_data_train_label, k = 1500)


CrossTable(x=covid_data_test_label, y=covid_data_death_pred, prop.chisq = FALSE)