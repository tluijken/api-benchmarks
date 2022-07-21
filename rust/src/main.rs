#[macro_use]
extern crate diesel;
#[macro_use]
extern crate diesel_migrations;

use std::{error::Error, net::Ipv4Addr};

use actix_web::{middleware::Logger, web::Data, App, HttpServer};
mod models;

mod api;
mod repository;
mod schema;

embed_migrations!();
#[actix_web::main]
async fn main() -> Result<(), impl Error> {
    env_logger::init();

    let pool = repository::db_context::get_pool();
    {
        // This will run the necessary migrations.
        match embedded_migrations::run(&pool.get().unwrap()) {
            Ok(()) => println!("Succesfully applied pending migrations (if any)"),
            Err(_) => println!("Unable to apply pending migrations"),
        }
    }
    // Make instance variable of ApiDoc so all worker threads gets the same instance.
    HttpServer::new(move || {
        // This factory closure is called on each worker thread independently.
        App::new()
            .app_data(Data::new(pool.clone()))
            .wrap(Logger::default())
            .configure(api::todo_controller::configure())
    })
    .bind((Ipv4Addr::UNSPECIFIED, 8080))?
    .run()
    .await
}
