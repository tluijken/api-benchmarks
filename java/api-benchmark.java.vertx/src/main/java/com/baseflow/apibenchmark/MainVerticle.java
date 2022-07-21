package com.baseflow.apibenchmark;

import io.vertx.core.AbstractVerticle;
import io.vertx.ext.web.Router;
import io.vertx.pgclient.PgConnectOptions;
import io.vertx.pgclient.PgPool;
import io.vertx.sqlclient.PoolOptions;
import io.vertx.sqlclient.Row;
import io.vertx.sqlclient.RowSet;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

public class MainVerticle extends AbstractVerticle {
    @Override
    public void start() throws Exception {
        // Create a Router
        PgConnectOptions connectOptions = new PgConnectOptions()
                .setPort(5432)
                .setHost("localhost")
                .setDatabase("todo_api")
                .setUser("todo_api_rw")
                .setPassword("hello_rust");

// Pool options
        PoolOptions poolOptions = new PoolOptions()
                .setMaxSize(50);

// Create the client pool

        Router router = Router.router(vertx);
        PgPool pool = PgPool.pool(vertx, connectOptions, poolOptions);

        // Mount the handler for all incoming requests at every path and HTTP method
        router.route("/todo").handler(context -> {
            // Get the address of the request

            List<Todo> todos = new ArrayList<Todo>(5);
            pool
                    .query("SELECT * FROM todos")
                    .execute(ar -> {
                        if (ar.succeeded()) {
                            RowSet<Row> result = ar.result();
                            Iterator<Row> iter = result.iterator();
                            while (iter.hasNext()) {
                                Row row = iter.next();
                                int id = row.getInteger( 0);
                                String value = row.getString( 1);
                                boolean checked = row.getBoolean(2);

                                todos.add(new Todo(id, value, checked));
                            }
                        }
                        context.json(todos);
                    });
            pool.close();
        });

        // Create the HTTP server
        vertx.createHttpServer()
                // Handle every request using the router
                .requestHandler(router)
                // Start listening
                .listen(8081)
                // Print the port
                .onSuccess(server ->
                        System.out.println(
                                "HTTP server started on port " + server.actualPort()
                        )
                );
    }
}