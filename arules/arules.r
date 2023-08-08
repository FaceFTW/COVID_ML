library(arules)
covid_data <- read.transactions("filtered_data_arules.csv", format = "basket", sep = ",")
summary(covid_data)
itemFrequencyPlot(covid_data, topN = 20, type = "absolute")
image(sample(covid_data, 1000), main = "Sample of 1k Random Cases", aspect="fill")

rules <- apriori(covid_data, parameter = list(supp = 0.001, conf = 0.8, minlen = 4, maxlen = 10))
summary(rules)

# Subset rules for deaths/Survived
death_rules <- subset(rules, subset = rhs %pin% "Death")
survived_rules <- subset(rules, subset = rhs %pin% "Survived")

inspect(sort(death_rules, by="lift"))
inspect(sort(survived_rules, by="lift"))