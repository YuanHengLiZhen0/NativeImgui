package com.netease.imguiUnity;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.TextView;

import com.netease.imguiUnity.databinding.ActivityMainBinding;

import java.util.logging.Logger;

public class MainActivity extends AppCompatActivity {

    // Used to load the 'imguiUnity' library on application startup.
    static {
        System.loadLibrary("cimgui");
    }

    private ActivityMainBinding binding;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        System.out.println("111111111111111");
        System.out.println(igGetTime());
        binding = ActivityMainBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());

        // Example of a call to a native method
        TextView tv = binding.sampleText;
        tv.setText(stringFromJNI());

        System.out.println(igGetTime());
    }

    /**
     * A native method that is implemented by the 'imguiUnity' native library,
     * which is packaged with this application.
     */
    public native String stringFromJNI();

    public native double igGetTime();
}