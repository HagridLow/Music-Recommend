package com.example.metalhead_testap

data class User (
    var username: String,
    var displayName: String,
    var token: String,
    var image: String
)

data class UserRegister(
    var email: String,
    var password: String,
    var displayName: String,
    var username: String
)