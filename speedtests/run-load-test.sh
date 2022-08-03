docker-compose up -d influxdb grafana
echo "--------------------------------------------------------------------------------------"
echo "Load testing with Grafana dashboard http://localhost:3000/d/k6/k6-load-testing-results"
echo "--------------------------------------------------------------------------------------"

# make sure we start clean
docker-compose --file ../docker-compose.yml down

# start only the database and the todo api in go
docker-compose --file ../docker-compose.yml up -d db todo_api_go
docker-compose run --rm k6 run /scripts/go.js
# cleanup after our test
docker-compose --file ../docker-compose.yml down


#repeat for all other tests
docker-compose --file ../docker-compose.yml up -d db todo_api_rust
docker-compose run --rm k6 run /scripts/rust.js
docker-compose --file ../docker-compose.yml down

docker-compose --file ../docker-compose.yml up -d db todo_api_node
docker-compose run --rm k6 run /scripts/node.js
docker-compose --file ../docker-compose.yml down

docker-compose --file ../docker-compose.yml up -d db todo_api_dotnet
docker-compose run --rm k6 run /scripts/dotnet.js
docker-compose --file ../docker-compose.yml down
