import postgres from 'postgres'

export class Database {

    public sql;

    constructor() {
        this.sql = postgres(process.env.DATABASE_URL ?? 'postgres://todo_api_rw:hello_rust@localhost:5432/todo_api');
    }
}
