# LSEG.StockMarketAnalyst

## Design

	Web API or Windows App ?

		accessible over web
		scalable
		can be deployed on cloud
				
	Infra

		server less functions maybe ?
		Request based sampling / Background hosted service

	Persistance
	
		Should the sampled data be persisted?
		should the output csv be persisted?

	Data Source
		
		Read from directory
		lets use the convention to read data files 
		</data/<parentfolder>/<exchangeName>/<stock name.csv>
	
	Security
		Use azure vault for secrets

	Tests
		Write L0 tests
	

### Modules

	Sampler Service

	1. File Reader
	2. Data sampler
	3. kafka producer

	Outlier Service

	1. Outlier Detector
	2. File Writer
	3. Kafka consumer

### How to run the app in local?
`- Install .NET 8 runtime/SDK both`
`- Install docker desktop`
`- Install Visual Studio 2022`

`- git clone the below repo in your local VS`
<https://github.com/18too90/LSEG.StockMarketAnalyst.git>

`- checkout the develop branch`
`- go to the solution folder where repo was created eg. <your repo root path>/LSEG.StockMarketAnalyst>`
`- Open terminal`
`- run docker-compose up -d`
`- browse`
<https://localhost:8081/DataSampler?countOfFilesToProcessPerExchange=2>
`- browse`
<https://localhost:5001/OutlierDetector>

#### Improvements Pending

###### Tests
###### Schema Registry for Kafka topics
###### remove secrets and move to azure

###### Use mongo db as event source 
###### UI for downloading outliers 




