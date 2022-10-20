# ClockifyTimeTracker BackEnd

ClockifyTimeTracker should help track time on clockify.

## Installation

### First run MongoDb 

Use the cmd to run docker MongoDb

```bash
docker pull mongo:latest
mkdir ..\mongodb-docker
cd ..\mongodb-docker
docker run -d -p 2717:27017 -v ~/mongodb-docker:/data/db --name mymongo mongo:latest
docker exec -it mymongo bash
mongosh
use ClockifyTimeTrackerLocalDb
exit
exit
```