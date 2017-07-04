# ORM Comparison
This project compares ORM code style and performance when executing stored procedures and mapping the results to existing POCOs

Out of scope are any comparisons for mapping the whole database or generate SQL for database interactions. Athough the tests could be expanded to do this if you want.

## Setup
I've included a database script for creating the database objects and data.

The tests all work with a single table of 300 records containing Int, String, Datetime, Boolean and Guid. There are 3 stored procedures

- `GetSingleRecord` pulls out a single record from the 300
- `GetAllRecords` pulls out all 300 records
- `ExecuteNonQuery` takes a parameter of each type plus an output parameter (to represent selecting the id of a newly inserted record). It doesn't return anything

## The Tests
Using each ORM each Stored Procedure is timed while being executed 5000 times. Before the timed run each Store Procedure is executed 500 times to allow the ORM and Database to warm up.

## The Results
As important as speed is the look of the code, but beauty is in the eye of the beholder so you will have to look at the source yourself.

Orm                      	|SelectOne       	|SelectAll       	|RunNonQuery
----------------------------|-------------------|-------------------|---------------
LinqToSql                	|00:00:01.9562822	|00:00:03.6997025	|00:00:01.6399206
EntityFramework          	|00:00:01.2706107	|00:00:03.4185752	|00:00:01.7025083
RawDataAccess            	|00:00:00.4961315	|00:00:02.9706760	|00:00:00.3808327
Dapper                   	|00:00:00.4071070	|00:00:02.5303710	|00:00:00.4087079
PetaPoco                 	|00:00:00.6143251	|00:00:02.8285628	|00:00:00.6723926
NPoco                    	|00:00:01.6210925	|00:00:03.4482741	|00:00:00.7048360
MicroLite*               	|00:00:00.4212418	|00:00:02.7869338	|00:00:00.4328647
Debonair*                	|00:00:00.5308836	|00:00:13.3562763	|00:00:00.4040702
ServiceStack.OrmLite     	|00:00:00.4229755	|00:00:02.4594527	|00:00:00.4004213

### * False Starts
Debonair and Microlite are included in the results, however, there doesn't seem to be a way to get the value of an output parameter using them, so really ... they fail the test.
