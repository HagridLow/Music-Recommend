package com.example.metalhead_testap

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.example.metalhead_testap.databinding.ActivityEntryPageBinding

private lateinit var binding: ActivityEntryPageBinding

class EntryPage : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityEntryPageBinding.inflate(layoutInflater)
        setContentView(binding.root)
    }
}