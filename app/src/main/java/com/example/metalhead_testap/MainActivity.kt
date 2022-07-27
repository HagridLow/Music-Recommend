package com.example.metalhead_testap

import android.os.Bundle
import android.view.View
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.Fragment
import com.example.metalhead_testap.databinding.ActivityEntryPageBinding
import com.example.metalhead_testap.databinding.ActivityMainBinding
import com.example.metalhead_testap.databinding.FragmentSearchBinding
import com.google.android.material.dialog.MaterialAlertDialogBuilder

const val TAG = "MainActivity"

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding

    private lateinit var binding2: ActivityEntryPageBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        binding2 = ActivityEntryPageBinding.inflate(layoutInflater)
        setContentView(binding.root)
        setContentView(binding2.root)



        binding.bottomNavigationView.setOnItemSelectedListener() {

            when (it.itemId) {

                R.id.home -> replaceFragment(Home())
                R.id.search -> replaceFragment(Search())
                R.id.profile -> replaceFragment(Profile())

                else -> {

                }
            }
            true
        }

    }



    private fun replaceFragment(fragment: Fragment) {
        val fragmentManager = supportFragmentManager
        val fragmentTransaction = fragmentManager.beginTransaction()
        fragmentTransaction.replace(R.id.frameLayout, fragment)
        fragmentTransaction.commit()
    }

}