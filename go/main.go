package main

import (	
    "database/sql"
	"encoding/json"
    "os"
	"fmt"
	"log"
    "github.com/joho/godotenv"
	"net/http"

	_ "github.com/lib/pq"
)

var db *sql.DB // Note the sql package provides the namespace

func init() {
    fmt.Println("Initializing")
    godotenv.Load()
    host := os.Getenv("DB_HOST")
    dbname := os.Getenv("DB_NAME")
    user := os.Getenv("DB_USER")
    password := os.Getenv("DB_PASSWORD")

	psqlInfo := fmt.Sprintf("host=%s port=%d user=%s "+
		"password=%s dbname=%s sslmode=disable",
		host, 5432, user, password, dbname)
    
    var err error
	db, err = sql.Open("postgres", psqlInfo)
	if err != nil {
		panic(err)
	}

	err = db.Ping()
	if err != nil {
		panic(err)
	}

    db.SetConnMaxLifetime(0)
    db.SetMaxIdleConns(3)
	db.SetMaxOpenConns(10)
    fmt.Println("Successfully connected to the database")
}

func GETHandler(w http.ResponseWriter, r *http.Request) {
    rows, err := db.Query("SELECT * FROM todos")
    if err != nil {
        log.Fatal(err)
    }

    var todos []Todo

    for rows.Next() {
        var todo Todo
        rows.Scan(&todo.Id, &todo.Value, &todo.Checked)
        todos = append(todos, todo)
    }

    todoBytes, _ := json.MarshalIndent(todos, "", "\t")

    w.Header().Set("Content-Type", "application/json")
    w.Write(todoBytes)

    defer rows.Close()
}

func main() {
    
    defer db.Close() //optional
    // Load the .env file in the current directory
	httpPort := os.Getenv("HTTP_PORT")
	if httpPort == "" {
		httpPort = "8081"
	}

    http.HandleFunc("/todo", GETHandler)
    log.Fatal(http.ListenAndServe(":" + httpPort, nil))
}

type Todo struct {
    Id int
    Value string
    Checked bool
}
