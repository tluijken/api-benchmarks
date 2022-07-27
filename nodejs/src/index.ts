import http from "node:http";
import {Database} from "./database/database";
import {TodoEntity} from "./database/todo.entity";


async function main() {
    // Setup DB client
    const db = new Database();

    //Verify DB connection
    try {
        await db.sql`SELECT 1=1`
    } catch (e) {
        console.error(`Failed connecting to database.`)
    }

    const server = http.createServer(async (req, res) => {
        //Query DB and fetch result
        const sqlResult: Array<TodoEntity> = await db.sql<Array<TodoEntity>>`SELECT * FROM todos;`;

        // Write response
        res.writeHead(200);
        res.end(JSON.stringify(sqlResult));
    });

    // Start http server
    console.log('Running server on port 1337');
    server.listen(1337);
}

main();
