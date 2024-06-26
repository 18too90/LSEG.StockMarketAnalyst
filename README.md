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

	Outlier Service

	1. Outlier Detector
	2. File Writer

