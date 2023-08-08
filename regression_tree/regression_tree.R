#########################################
# Data Import + Prep
#########################################
covid_data <- read.csv("weekly_deaths_converted_data.csv", stringsAsFactors = TRUE)
# covid_data$state <- as.numeric(covid_data$state)
covid_data <- covid_data[-1]

# All other data should be coerced into numeric/int by default
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
training_count <- 8380
testing_count <- 2000

# After adjustment data seems to be ordered, so we need to shuffle it
print("Initializing RNG with seed");
RNGversion("3.6.0");
set.seed(1234)

train_rows <- sample(1:nrow(covid_data), training_count)
print("splitting")
covid_data_train <- covid_data[train_rows,]
covid_data_test <- covid_data[-train_rows,]


########################
# RPART - Regression Tree Time!
########################
library(rpart)
library(rpart.plot)

model_default <- rpart(total_deaths ~ ., data = covid_data_train)
predict_default <- predict(model_default, covid_data_test)

# Plot the tree
rpart.plot(model_default, digits = 3, fallen.leaves = TRUE, type = 4, extra = 101)

# summarize results
summary(covid_data_test$total_deaths)
summary(predict_default) 
cor(predict_default, covid_data_test$total_deaths)

