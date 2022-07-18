package com.example.metalhead_testap

import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object ApiInstance {
    val api: ApiInterface by lazy {
        Retrofit.Builder()
            .baseUrl("https://localhost:7035/api/")
            .addConverterFactory(GsonConverterFactory.create())
            .build()
            .create(ApiInterface::class.java)
    }
}