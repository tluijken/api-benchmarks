use super::*;
use diesel::Queryable;
/// Task to do.
#[derive(Serialize, Deserialize, Clone, Debug, Queryable)]
pub struct Todo {
    /// Unique id for the todo item.
    pub id: i32,
    /// Description of the taks to do.
    pub value: String,
    /// Mark is the task done or not
    pub checked: bool,
}

/// Request to update existing `Todo` item.
#[derive(Serialize, Deserialize, Clone, Debug)]
pub struct TodoUpdateRequest {
    /// Optional new value for the `Todo` task.
    pub value: Option<String>,
    /// Optional check status to mark is the task done or not.
    pub checked: Option<bool>,
}
