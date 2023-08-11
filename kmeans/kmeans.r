# Normalization function
normalize <- function(x) {
    return((x - min(x)) / (max(x) - min(x)))
}

#########################################
# Data Import + Prep
#########################################
covid_data <- read.csv("filtered_data_kmeans.csv", stringsAsFactors = TRUE)
str(covid_data)
table(covid_data$age_group)
covid_data$age_group <- factor(covid_data$age_group, levels = c(
    "0 - 17 years",
    "18 to 49 years",
    "50 to 64 years",
    "65+ years",
    "Missing"
))
covid_data$symptom_status <- factor(covid_data$symptom_status, levels = c(
    "Asymptomatic", "Symptomatic"
))
covid_data$exposure_yn <- factor(covid_data$exposure_yn, levels = c(
    "Unknown", "Yes"
))
covid_data$hosp_yn <- factor(covid_data$hosp_yn, levels = c("No", "Yes"))
covid_data$icu_yn <- factor(covid_data$icu_yn, levels = c("No", "Yes"))
covid_data$death_yn <- factor(covid_data$death_yn, levels = c("No", "Yes"))
covid_data$underlying_conditions_yn <- factor(covid_data$underlying_conditions_yn, levels = c(
    "No", "Yes"
))

covid_data <- covid_data[-2]
# coerce for normalization
covid_data_norm <- as.data.frame(lapply(covid_data, as.numeric))
covid_data_norm <- as.data.frame(lapply(covid_data_norm, normalize))

str(covid_data)

# check for blanks
if (sum(is.na(covid_data_norm)) > 0) {
    stop("There are blanks in the data")
} else {
    print("There are no blanks in the data. Showing Preview:")
    str(covid_data_norm)
}
table(covid_data$icu_yn)

#########################################
# k-Means Clustering
#########################################
print("Initializing RNG with seed")
RNGversion("3.6.0")
set.seed(1234)

clusters <- kmeans(covid_data_norm, 5)
clusters$size
clusters$centers
