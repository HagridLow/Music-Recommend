package com.example.metalhead_testap

import retrofit2.Call
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.GET
import retrofit2.http.Query



interface ApiInterface {
    val search: String
    @GET("search/getalbum/")
    suspend fun getSearchedAlbums(@Query("search") keyword: String) : Response<List<Album>>

}
