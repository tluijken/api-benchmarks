# Language benchmarks WebAPI

This repo is used to test various language WebAPI applications.

## Building the images
To build the latest images run

```shell
docker-compose build
```

## Running your api's
to run the api's for the compiled docker containers run

```shell
docker-compose up
```

Don't worry, the database will be created and seeded with initial data for you.

## Running the load tests
If you want to run a test, head over to the speedtests folder and run the bash or powershell script (depending on your OS).


```shell
./speedtests/run-load-test.sh
```

## Check the results
To see the results, open up your browser to [http://localhost:3000/d/k6/k6-load-testing-results](http://localhost:3000/d/k6/k6-load-testing-results)

To add a new language, just create a folder, with your project. Make sure to embed it in a docker container and embed that to the docker-compose file. Keep the portnumbers in mind.
You can add a test in the speedtests folder for your language WebAPI project, and make sure it get's triggered in the run-load-test script.

