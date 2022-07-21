package com.baseflow.apibenchmark;

public class Todo {

    private int id;
    private String value;
    private boolean checked;

    public Todo(int id, String value, boolean checked) {
        this.id = id;
        this.value = value;
        this.checked = checked;
    }

    public int getId() {
        return id;
    }

    public String getValue() {
        return value;
    }

    public boolean isChecked() {
        return checked;
    }
}
