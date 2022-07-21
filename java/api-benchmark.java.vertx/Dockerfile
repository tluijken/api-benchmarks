FROM maven:3.8.6-jdk-8 AS build

WORKDIR /build

COPY ./ ./
RUN mvn dependency:resolve dependency:resolve-plugins clean install package

FROM vertx/vertx4 AS runtime

WORKDIR /usr/verticles

EXPOSE 8081

# Copy your verticle to the container                   (2)
COPY --from=build /build/target/api-benchmark.java.vertx-1.0-SNAPSHOT-fat.jar /usr/verticles/
ENTRYPOINT ["sh", "-c"]
CMD ["exec vertx run com.baseflow.apibenchmark.MainVerticle -cp /usr/verticles/*"]