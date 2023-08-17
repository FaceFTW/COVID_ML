# COVID Machine Learning Experiments

Some experiments with various machine learning models to predict COVID-19 Deaths in the US. This was a part of a machine learning course I took at Rose-Hulman Institute of Technology.

THIS IS CURRENTLY A WORK IN PROGRESS. I PLAN ON CLEANING UP THIS STUFF IN A COUPLE OF WEEKS.

## Tools Used

The main notebook is actually a .NET Interactive/Polyglot Notebook which uses the Juptyer IRkernel to run R code. Separate C# and R scripts are provided as well. TODO - add devcontainer for VS Code.


# Setup Instructions

In order to run the code in this repository, you will need to install the following dependencies:

- [R](https://www.r-project.org/)
- [RStudio](https://rstudio.com/) (*optional*)
- [.NET](https://dotnet.microsoft.com/download)
- [.NET Interactive](https://github.com/dotnet/interactive)
  - *This can be installed as a global tool with `dotnet tool install -g Microsoft.dotnet-interactive`*

To use the Polyglot Notebook (the `*.dib` file), you will need to install the following:
- [Jupyter](https://jupyter.org/)
  - `pip install notebook`
- [IRkernel](https://github.com/IRkernel/IRkernel)
  - `R -e "IRkernel::installspec()"`
- VS Code
  - [Jupyter Extension](https://marketplace.visualstudio.com/items?itemName=ms-toolsai.jupyter)
  - [R Extension](https://marketplace.visualstudio.com/items?itemName=Ikuyadeu.r)
  - [Polyglot Notebooks Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.dotnet-interactive-vscode)

# Data Sources
I used the following data sources for this project: (Includes a PowerShell Line to download the data)
## Main Dataset
Name: COVID-19 Case Surveillance Public Use Data with Geography
Accessed From: https://data.cdc.gov/Case-Surveillance/COVID-19-Case-Surveillance-Public-Use-Data-with-Ge/n8mc-b4w4
Rows: 99M, File Size: 13.7GB
Used With: kNN, Decision Trees, Naive Bayes, Neural Network, Association Rules
```powershell
Invoke-WebRequest -Uri "https://data.cdc.gov/api/views/n8mc-b4w4/rows.csv?accessType=DOWNLOAD" -OutFile "COVID-DATA.csv"
```

## Alternate Dataset
Name: Weekly United States COVID-19 Cases and Deaths by State - ARCHIVED
Accessed From:https://data.cdc.gov/Case-Surveillance/Weekly-United-States-COVID-19-Cases-and-Deaths-by-/pwn4-m3yp
Rows: 10k, File Size: 598KB

```powershell
Invoke-WebRequest -Uri "https://data.cdc.gov/api/views/pwn4-m3yp/rows.csv?accessType=DOWNLOAD" -OutFile "weekly_covid_data.csv"
```