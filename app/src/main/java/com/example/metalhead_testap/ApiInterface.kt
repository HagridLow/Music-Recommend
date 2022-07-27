package com.example.metalhead_testap

import retrofit2.Call
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.*


interface ApiInterface {
    @GET("search/getalbum")
    suspend fun getSearchedAlbums(@Query("search") keyword: String) : Response<List<Album>>

    @POST("account/register")
    suspend fun registerUser(
        @Field("email") email: String)


}
