// Needed for musl builds.
extern crate openssl;
#[macro_use]
extern crate diesel;

use std::{error::Error, net::Ipv4Addr};

use actix_web::{middleware::Logger, web::Data, App, HttpServer};
mod models;

mod api;
mod repository;
mod schema;

#[actix_web::main]
async fn main() -> Result<(), impl Error> {
    env_logger::init();

    HttpServer::new(move || {
        // This factory closure is called on each worker thread independently.
        App::new()
            .app_data(Data::new(repository::db_context::get_pool()))
            .wrap(Logger::default())
            .configure(api::todo_controller::configure())
    })
    .bind((Ipv4Addr::UNSPECIFIED, 8080))?
    .run()
    .await
}
