#########################################
# Data Import + Prep
#########################################
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

print("Initializing RNG with seed");
RNGversion("3.6.0");
set.seed(1234)

train_rows <- sample(1:nrow(covid_data), training_count)

covid_data_train <- covid_data[train_rows,1:9]
covid_data_test <- covid_data[-train_rows,1:9]

# check proportions
print("Proportions of Datasets:")
prop.table(table(covid_data_train$death_yn))
prop.table(table(covid_data_test$death_yn))

########################
# Decision Tree Time!
########################
library(C50)

model <- C5.0(covid_data_train[-9], covid_data_train$death_yn, trials = 25)
summary(model)

predictions <- predict(model, covid_data_test, type = "class")

library(gmodels)
CrossTable(x=predictions, y=covid_data_test$death_yn, prop.chisq = FALSE, prop.c = FALSE, prop.r = FALSE, dnn = c('predicted', 'actual'))