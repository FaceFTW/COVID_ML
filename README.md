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