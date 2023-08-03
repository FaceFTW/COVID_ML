# Normalization function
normalize <- function(x) {
  return ((x - min(x)) / (max(x) - min(x)))
}

#########################################
# Data Import + Prep
#########################################
covid_data <- read.csv("filtered_data_nn.csv", stringsAsFactors = TRUE)
covid_data <- covid_data[-2]
# coerce for normalization
covid_data <- as.data.frame(lapply(covid_data, as.numeric))
covid_data_norm <- as.data.frame(lapply(covid_data, normalize))

# check for blanks
if(sum(is.na(covid_data)) > 0) {
	stop("There are blanks in the data")
} else {
  print("There are no blanks in the data. Showing Preview:")
  str(covid_data_norm)
}

#########################################
# Separating Training and Testing Data
#########################################
training_count <- 50000
testing_count <- 10000

print("Initializing RNG with seed");
RNGversion("3.6.0");
set.seed(1234)

subset <- sample(1:nrow(covid_data_norm), training_count + testing_count)
train_rows <- subset[1:training_count]
test_rows <- subset[(training_count + 1):(training_count + testing_count)]

covid_data_train <- covid_data_norm[train_rows,1:9]
covid_data_test <- covid_data_norm[test_rows,1:9]


########################
# Neural Net Time!
########################
library(neuralnet)

covid_model_non_linear <- neuralnet(death_yn ~ weeks_since + age_group
						+sex + exposure_yn + symptom_status + hosp_yn
						+ icu_yn + underlying_conditions_yn,
						data=covid_data_train,
						hidden=5,
						threshold=0.015,
						act.fct = "logistic",
						lifesign = "full",
						lifesign.step = 1000,
						)
predictions <- predict(covid_model, covid_data_test[1:9])

# Plot the model
plot(covid_model)

# # summarize results
# print("Summary (Test Data Predicted):")
summary(table(covid_model$net.result))
# print("Summary (Test Data Actual):")
summary(table(covid_data_test$death_yn))
