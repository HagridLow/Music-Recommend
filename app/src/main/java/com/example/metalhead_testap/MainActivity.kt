package com.example.metalhead_testap

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.Menu
import android.view.MenuInflater
import androidx.appcompat.widget.SearchView
import androidx.core.view.isVisible
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.metalhead_testap.databinding.ActivityMainBinding
import retrofit2.HttpException
import java.io.IOException


const val TAG = "MainActivity"

class MainActivity : AppCompatActivity() {

    private lateinit var binding: ActivityMainBinding

    private lateinit var albumAdapter : AlbumAdapter



    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)
        setupRecyclerView()

        val searchView = binding.searchView
        searchView.setOnQueryTextListener(object: SearchView.OnQueryTextListener{
            override fun onQueryTextSubmit(keyword: String?): Boolean {
                lifecycleScope.launchWhenCreated {
                    binding.progressBar.isVisible = true
                    val response = try {
                        binding.recyclerView.scrollToPosition(0)
                        ApiInstance.api.getSearchedAlbums(keyword!!)
                    } catch (e: IOException) {
                        Log.e(TAG, "IOException, you might not have internet connection")
                        binding.progressBar.isVisible = false
                        return@launchWhenCreated
                    } catch (e: HttpException) {
                        Log.e(TAG, "HttpException, unexpected response")
                        binding.progressBar.isVisible = false
                        return@launchWhenCreated
                    }
                    if(response.isSuccessful && response.body() != null){
                        albumAdapter.albums = response.body()!!
                    } else {
                        Log.e(TAG, "Response not successful")
                    }
                    binding.progressBar.isVisible = false
                }
                return true
            }

            override fun onQueryTextChange(newText: String?): Boolean {
                return true
            }

        })

    }




    private fun setupRecyclerView() = binding.recyclerView.apply() {
        albumAdapter = AlbumAdapter()
        adapter = albumAdapter
        layoutManager = LinearLayoutManager(this@MainActivity)
    }

 }
