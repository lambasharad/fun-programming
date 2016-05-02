package woodbug.pnr.enquiry;

import java.util.ArrayList;
import java.util.Locale;
import android.annotation.TargetApi;
import android.app.Activity;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.speech.tts.TextToSpeech;
import android.speech.tts.TextToSpeech.OnInitListener;
import android.util.Log;
import android.view.KeyEvent;
import android.widget.TextView;
import android.widget.Toast;

@TargetApi(Build.VERSION_CODES.GINGERBREAD)
public class ShowStatus extends Activity implements OnInitListener {

	TextView result;
	StringBuffer status, temp,final_status;
	private final int REQ_CODE_SPEECH_INPUT = 100;
	private TextToSpeech tts;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.status);
		status = new StringBuffer();
		temp = new StringBuffer();
		result = (TextView) findViewById(R.id.result);
		tts = new TextToSpeech(this, this);
		Intent intent = getIntent();

		try {
			temp.append(intent.getStringExtra("json"));

			for (int i = 0; i < temp.length(); i++) {
				if (temp.charAt(i) == '\\' && temp.charAt(i + 1) == 'u'
						&& temp.charAt(i + 2) == '0'
						&& temp.charAt(i + 3) == '0'
						&& temp.charAt(i + 4) == '0'
						&& temp.charAt(i + 5) == 'a')
					temp.delete(i, i + 6);
			}

			for (int i = 0; i < temp.length(); i++) {
				if (temp.charAt(i) == '<') {
					if (status.charAt(status.length() - 1) != '\n')
						status.append('\n');
					while (i < temp.length() && temp.charAt(i) != '>') {
						i++;
					}
				}
				else if (temp.charAt(i) == '(') {
					while (i < temp.length() && temp.charAt(i) != ')') {
						i++;
					}
				} 
					else {
				
					status.append(temp.charAt(i));
				}
			}
			
			ArrayList<StringBuffer> al = new ArrayList<StringBuffer>();
			int i = 0;
			while (status.charAt(i) != '\n')
				i++;
			i++;
			for (int count = 0; count < 8; count++) {

				temp = new StringBuffer();
				for (; status.charAt(i) != '\n'; i++) {
                 temp.append(status.charAt(i));
				}i++;
				al.add(temp);
			}
			for (int count = 0; count < 8; count++) {

				temp = new StringBuffer();
				for (; status.charAt(i) != '\n'; i++) {
                 temp.append(status.charAt(i));
				}i++;
				al.get(count).append(" - ").append(temp);
			}
			final_status=new StringBuffer();
			for(int k=0;k<al.size();k++)
				final_status.append('\n').append(al.get(k));
			
			
				al = new ArrayList<StringBuffer>();
				for (int count = 0; count < 4; count++) {

					temp = new StringBuffer();
					for (; status.charAt(i) != '\n'; i++) {
						temp.append(status.charAt(i));
					}
					i++;
					al.add(temp);
				}
				al.remove(2);

				while (status.charAt(i)=='P') {
					final_status.append('\n');
				for (int count = 0; count < 3; count++) {

					temp = new StringBuffer();
					for (; status.charAt(i) != '\n'; i++) {
						temp.append(status.charAt(i));
					}
					i++;
					final_status.append('\n').append(al.get(count)+" - "+temp);
				}
			}
				
			final_status.append("\n\n").append(status.substring(i, status.length()));
			

			if (final_status.equals(""))
				throw new Exception();
			else
				result.setText(final_status);
		} catch (Exception ignore) {
			Toast.makeText(getApplicationContext(),
					"Invalid Result. check PNR number..", Toast.LENGTH_LONG)
					.show();
		}
	}

	@Override
	public void onInit(int status) {
		// TODO Auto-generated method stub
		if (status == TextToSpeech.SUCCESS) {

			int result = tts.setLanguage(Locale.US);
			
			if (result == TextToSpeech.LANG_MISSING_DATA
					|| result == TextToSpeech.LANG_NOT_SUPPORTED) {
				Toast.makeText(this, "Language not supported", Toast.LENGTH_LONG).show();
				Log.e("TTS", "Language is not supported");
			} //else {
				//Speaker_check_button.setEnabled(true);

			//}

		} else {
			Log.e("TTS", "Initilization Failed");
		}
	}
	private void speakOut() {
		// TODO Auto-generated method stub
		
		String text = result.getText().toString();
		if (text.length() == 0) {
			tts.speak("You haven't type correct pnr number", TextToSpeech.QUEUE_FLUSH, null);
		} else {
			//tts.speak("Your PNR number is", TextToSpeech.QUEUE_FLUSH, null);
			tts.speak( text , TextToSpeech.QUEUE_FLUSH, null);
			tts.setSpeechRate((float) 0.8);
			
		}

	}
	public boolean dispatchKeyEvent(KeyEvent event) {
	    int action = event.getAction();
	    int keyCode = event.getKeyCode();
	        switch (keyCode) {
	        case KeyEvent.KEYCODE_VOLUME_UP:
	            if (action == KeyEvent.ACTION_DOWN) {
	            	
	            	//promptSpeechInput();
	            	 Intent i = new Intent(getApplicationContext(),speakpnr.class);
	            	 startActivity(i);
	            }
	            return true;
	        case KeyEvent.KEYCODE_VOLUME_DOWN:
	            
	        	 if (action == KeyEvent.ACTION_DOWN) {
		            	
		            	speakOut();
	        		/* String theText = result.getText().toString();
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

		            */	
		            }
	            return true;
	        default:
	            return super.dispatchKeyEvent(event);
	            
	            
	        }

}}