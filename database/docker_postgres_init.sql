create table __diesel_schema_migrations
(
    version varchar(50)                         not null
        primary key,
    run_on  timestamp default CURRENT_TIMESTAMP not null
);


alter table __diesel_schema_migrations owner to todo_api_rw;
insert into __diesel_schema_migrations (version) values ('00000000000000');
insert into __diesel_schema_migrations (version) values ('20220715124543');

create table todos
(
    id      serial
        primary key,
    value   text                  not null,
    checked boolean default false not null
);

insert into todos (id, "value", checked) values (1, 'VIM plugins bijwerken', false);
insert into todos (id, "value", checked) values (2, 'Code inchecken in github', true);
insert into todos (id, "value", checked) values (3, 'Uren inchecken in timechimp', false);
insert into todos (id, "value", checked) values (4, 'PR review collega dev', false);
insert into todos (id, "value", checked) values (5, 'Release voor klant voorbereiden', false);
insert into todos (id, "value", checked) values (6, 'Dependencies updaten in project', false);
insert into todos (id, "value", checked) values (7, 'Dotnet upgraden naar dotnet 7', true);
insert into todos (id, "value", checked) values (8, 'Kleinere docker image voor Rust maken', false);
insert into todos (id, "value", checked) values (9, 'Klant evaluatie voorbereiden', false);
insert into todos (id, "value", checked) values (10, 'Lets encrypt ingeregen in cluster', true);
insert into todos (id, "value", checked) values (11, 'Docker compose file genereren', false);
insert into todos (id, "value", checked) values (12, 'Gitignore bijwerken', false);
