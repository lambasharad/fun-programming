package woodbug.pnr.enquiry;

import java.util.ArrayList;
import java.util.Locale;

import woodbug.pnr.enquiry.R.string;
import android.annotation.SuppressLint;
import android.annotation.TargetApi;
import android.app.Activity;
import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.ActivityNotFoundException;
import android.content.Context;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.os.PowerManager;
import android.os.PowerManager.WakeLock;
import android.speech.RecognizerIntent;
import android.speech.tts.TextToSpeech;
import android.speech.tts.TextToSpeech.OnInitListener;
import android.util.Log;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.TextView;
import android.widget.Toast;



@TargetApi(Build.VERSION_CODES.GINGERBREAD)
public class speakpnr extends Activity implements OnInitListener {

	//protected static final int RESULT_SPEECH = 1;

	private ImageButton btnSpeak;
	//public TextView txtText;
	private final int REQ_CODE_SPEECH_INPUT = 100;
	private TextToSpeech tts;
	private Button Speaker_check_button;
	private EditText CopyText;
	private Button cont;
	private WakeLock wakeLock;
	
	
	
	

	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.speak_pnr);
		
		
		
		
		// btnSpeak = (ImageButton)findViewById(R.id.btnSpeak);
		 //txtText = (EditText)findViewById(R.id.txtText);
		 
		//getActionBar().hide();
		 
	       /* btnSpeak.setOnClickListener(new View.OnClickListener() {
	 
	            @Override
	            public void onClick(View v) {
	                promptSpeechInput();
	            }

				
	        });*/
	        
	        
		 
		 
		   tts = new TextToSpeech(this, this);
			Speaker_check_button = (Button) findViewById(R.id.Speaker_check_button);
			CopyText = (EditText) findViewById(R.id.CopyText);
			
			/*
		 
		 btnSpeak.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				
				Intent intent = new Intent(
						RecognizerIntent.ACTION_RECOGNIZE_SPEECH);

				intent.putExtra(RecognizerIntent.EXTRA_LANGUAGE_MODEL, "en-US");

				try {
					startActivityForResult(intent, RESULT_SPEECH);
					 txtText.setText("");	
				} catch (ActivityNotFoundException a) {
					Toast t = Toast.makeText(getApplicationContext(),
							"Ops! Your device doesn't support Speech to Text",
							Toast.LENGTH_SHORT);
					t.show();
			}}}
		);
	*/

		/* Speaker_check_button.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View arg0) {
				// TODO Auto-generated method stub
				
				speakOut();
				// Intent i = new Intent(getApplicationContext(),PNREnquiryActivity.class);
			     //  i.putExtra("location", CopyText.getText().toString());      
			      //  startActivity(i);
				
			}
			
			
			

			
		});*/
		 cont = (Button)findViewById(R.id.Continue);
		 
		 cont.setOnClickListener(new View.OnClickListener() {
				
				@TargetApi(Build.VERSION_CODES.GINGERBREAD)
				@Override
				public void onClick(View v) {
					String theText = CopyText.getText().toString();
				       Intent i = new Intent(getApplicationContext(),PNREnquiryActivity.class);
				       i.putExtra("location", theText);    
				       if(theText.isEmpty()){
				    	   Toast t = Toast.makeText(getApplicationContext(),
									"Ops! You did not enter PNR Number",
									Toast.LENGTH_SHORT);
							t.show();   
				      
				       }
				       else{
				        startActivity(i);
				       }
				}
			}); 
		 
		
		 
	}
	
		 
		
	
	@Override
	protected void onDestroy() {
		// TODO Auto-generated method stub
		super.onDestroy();
	}
	
	private void speakOut() {
		// TODO Auto-generated method stub
		
		String text = CopyText.getText().toString();
		if (text.length() == 0) {
			tts.speak("You haven't typed text", TextToSpeech.QUEUE_FLUSH, null);
		} else {
			//tts.speak("Your PNR number is", TextToSpeech.QUEUE_FLUSH, null);
			tts.speak("Your pnr number is" + text +"press power key once and if this is right press volume down to continue", TextToSpeech.QUEUE_FLUSH, null);
			tts.setSpeechRate((float) 0.8);
			
		}

	}
	
	/*private void speakOutafterlisten() {
		// TODO Auto-generated method stub
		
		
			tts.speak("Press volume up key to listen your pnr number and volume down key to continue ", 
					TextToSpeech.QUEUE_FLUSH, null);
		
*/
	
	
		

	@Override
	public void onInit(int status) {
		// TODO Auto-generated method stub
		if (status == TextToSpeech.SUCCESS) {

			int result = tts.setLanguage(Locale.US);
			
			if (result == TextToSpeech.LANG_MISSING_DATA
					|| result == TextToSpeech.LANG_NOT_SUPPORTED) {
				Toast.makeText(this, "Language not supported", Toast.LENGTH_LONG).show();
				Log.e("TTS", "Language is not supported");
			} else {
				Speaker_check_button.setEnabled(true);

			}

		} else {
			Log.e("TTS", "Initilization Failed");
		}
	}
	private void promptSpeechInput() {
		// TODO Auto-generated method stub
		Intent intent = new Intent(RecognizerIntent.ACTION_RECOGNIZE_SPEECH);
        intent.putExtra(RecognizerIntent.EXTRA_LANGUAGE_MODEL,
                RecognizerIntent.LANGUAGE_MODEL_FREE_FORM);
        intent.putExtra(RecognizerIntent.EXTRA_LANGUAGE, Locale.getDefault());
        intent.putExtra(RecognizerIntent.EXTRA_PROMPT,
                getString(R.string.speech_prompt));
        try {
            startActivityForResult(intent, REQ_CODE_SPEECH_INPUT);
        } catch (ActivityNotFoundException a) {
            Toast.makeText(getApplicationContext(),
                    getString(R.string.speech_not_supported),
                    Toast.LENGTH_SHORT).show();
        }
	}
	 @Override
	    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
	        super.onActivityResult(requestCode, resultCode, data);
	 
	        switch (requestCode) {
	        case REQ_CODE_SPEECH_INPUT: {
	            if (resultCode == RESULT_OK && null != data) {
	 
	                ArrayList<String> result = data
	                        .getStringArrayListExtra(RecognizerIntent.EXTRA_RESULTS);
	                CopyText.setText(result.get(0));
	                //CopyText.setText(result.get(0));
	            }
	            break;
	        }
	 
	        }
	 }
	
	 @Override
	    public boolean onCreateOptionsMenu(Menu menu) {
	        // Inflate the menu; this adds items to the action bar if it is present.
	        getMenuInflater().inflate(R.menu.main, menu);
	        return true;
	    }
	 public boolean dispatchKeyEvent(KeyEvent event) {
		    int action = event.getAction();
		    int keyCode = event.getKeyCode();
		        switch (keyCode) {
		        case KeyEvent.KEYCODE_VOLUME_UP:
		            if (action == KeyEvent.ACTION_DOWN) {
		            	
		            	promptSpeechInput();

		            	
		            }
		            return true;
		        case KeyEvent.KEYCODE_VOLUME_DOWN:
		            
		        	 if (action == KeyEvent.ACTION_DOWN) {
			            	
			            	//speakOut();
		        		 String theText = CopyText.getText().toString();
					       Intent i = new Intent(getApplicationContext(),PNREnquiryActivity.class);
					       i.putExtra("location", theText);    
					       if(theText.isEmpty()){
					    	   Toast t = Toast.makeText(getApplicationContext(),
										"Ops! You did not enter PNR Number",
										Toast.LENGTH_SHORT);
								t.show();   
								
					      
					       }
					       else{
					        startActivity(i);
					       };

			            	
			            }
		            return true;
		    case KeyEvent.KEYCODE_POWER:
		        {
		        /*	if (action == KeyEvent.) {
		        		String theText = CopyText.getText().toString();
					       Intent i = new Intent(getApplicationContext(),PNREnquiryActivity.class);
					       i.putExtra("location", theText);    
					       if(theText.isEmpty()){
					    	   Toast t = Toast.makeText(getApplicationContext(),
										"Ops! You did not enter PNR Number",
										Toast.LENGTH_SHORT);
								t.show();   
					      
					       }
					       else{
					        startActivity(i);
					       };
		        	}
		        }*/
		        	
		         if (event.getKeyCode() == KeyEvent.KEYCODE_POWER) {
		        		speakOut();
			            }
		        	 }
		        default:
		            return super.dispatchKeyEvent(event);
		            
		            
		        }
		       

	            
		       
		    
	
	 }
	 
	 

}
