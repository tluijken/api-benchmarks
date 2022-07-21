-- Your SQL goes here
CREATE TABLE todos (
  id SERIAL PRIMARY KEY,
  value TEXT NOT NULL,
  checked BOOLEAN NOT NULL DEFAULT 'f'
)
