package com.example.metalhead_testap

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.bumptech.glide.Glide
import com.example.metalhead_testap.databinding.ActivityAlbumDetailBinding

class AlbumDetailActivity : AppCompatActivity() {
    private lateinit var binding: ActivityAlbumDetailBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityAlbumDetailBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val album = intent.getParcelableExtra<Album>("album")
        if(album != null){
            Glide.with(binding.imageDetailView).load(album.image).into(binding.imageDetailView)
            binding.textViewName.text = album.name
            binding.textViewArtist.text = album.artist
            binding.textViewNumber.text = album.totalTracks.toString()
        }
    }


}