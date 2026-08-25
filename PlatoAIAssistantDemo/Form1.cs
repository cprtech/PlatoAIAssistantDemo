using Microsoft.Data.SqlClient;
using Plato.QueryEngine.Analysis;
using Plato.QueryEngine.Execution;
using PlatoChat;
using System.Data;


namespace PLatoAIAssistantTest
{
	public partial class Form1 : Form
	{
		private PlatoChatWindow pcw;

		string openAIApiKey = "[OpenAIKey]";

		SqlConnection conversationSqlConnection = new SqlConnection("[ConnectionString]");

		private string contosoConnectionString = "[ConnectonString]";

		public string OwnerId = "[UserGeneratedGuid]"; // User defined Guid
		private bool headless = false;

		public Form1()
		{
			InitializeComponent();

			this.Text = "Plato AI Assistant Demo";

			// Open the database used for persistent conversation storage
			conversationSqlConnection.Open();

			pcw = new PlatoChatWindow("Plato", OwnerId, OwnerId, openAIApiKey,
				null, // Null connection string if using open connection to Conversation database
					  // Otherwise, pass in the connection string to the database used for persistent conversation storage
					  // and pass null as the open connection
				conversationSqlConnection,
				//"https://[appendpoint].openai.azure.com/" if using Azure
				"https://api.openai.com/v1/" // use openAI
				);

			pcw.DomLoaded += onDomReady;

			if (headless)
			{
				// Prompts would be sent via the SendRemotePromptAsync method,
				// and answers would be received via the AnswerReceived event.
				pcw.CreateControl();
			}
			else
			{
				pcw.SetUserName("PlatoUser");

				pcw.Dock = DockStyle.Fill;
				this.Controls.Add(pcw);
				pcw.Show();
				pcw.BringToFront();
			}

			this.FormClosing += BrowserForm_FormClosing;
		}

		private void BrowserForm_FormClosing(object? sender, FormClosingEventArgs e)
		{
			pcw.DisposeClient();
		}

		private async Task<bool> onPromptSubmitted(string prompt)
		{
			// You can handle the prompt submission here. For example, you can log the prompt
			// or perform any other actions before it is sent to the AI model.
			// If you want to allow the prompt to be sent to the AI model, return false indicating it was
			// not processed by the program.

			return false;
		}

		private async Task onAnswerReceived(string answer)
		{
			// You can handle the answer received from the AI model here. For example, you can log the answer
			// or perform any other actions with it. THe answer will be the complete Html streamed answer.
		}

		private void onQueryAnalysisResultsReceived(in QueryAnalysisResult result)
		{
			// QueryAnalysisResult class is received
			// Log the information
			string q = result.Query.CommandText;
			DataTable dt = result.Data;
		}


		public void onDatabaseQueryStarted(object? sender, DatabaseQueryStartedEventArgs e)
		{
			//  A database query has begun. Log the necessary information
			bool haggr = e.SqlAnalysis.HasAggregate;
			// If true the engine has generated a query containing COUNT, MAX, MIN, etc.
		}

		public void onDatabaseQueryCompleted(object? sender, DatabaseQueryCompletedEventArgs e)
		{
			// Database query has been completed and e has the DataTable
			DataTable dt = e.Analysis.Data;
			if (dt != null)
			{
				// Process each row of the returned table results
			}
		}

		private async void onDomReady(object? sender, EventArgs args)
		{
			// Uncomment out the following to use custom properties

			//pcw.SetDefaultGreeting("This is the default greeting.");
			//pcw.SetPlatoIsThinkingText("Plato considering...");
			//pcw.SetTitle("New Plato AI Assistant");

			//pcw.SetLibraryVisibility(false);
			//pcw.SetFilesVisibility(false);

			//Image asst = Image.FromFile("C:\\temp\\assistant.png");
			//Image user = Image.FromFile("C:\\temp\\user.png");

			//await pcw.SetAssistantImage(asst);
			//await pcw.SetUserImage(user);

			// Set to user defined model list. Presumable read from a configuration file
			await pcw.SetToModelsList("gpt-5.6,gpt-5.6-terra,gpt-5.2,gpt-5.1");

			// Setup assistant handlers
			pcw.AnswerReceived = onAnswerReceived;

			pcw.PromptSubmitted = onPromptSubmitted;

			pcw.QueryAnalysisResultsReceived = onQueryAnalysisResultsReceived;

			pcw.DatabaseQueryStarted = onDatabaseQueryStarted;

			pcw.DatabaseQueryCompleted = onDatabaseQueryCompleted;

			pcw.DatabaseDataRetrieved = onDatabaseDataRetrieved;

			if (headless)
			{
				// Send a prompt to the assistant and wait for an answer on the handlers
				await SendRemotePrompt();
			}
		}

		private async Task SendRemotePrompt()
		{
			await pcw.SendRemotePromptAsync("Dynamic Tab", "Analyze the uploaded log files in VS:LogFiles", "gpt-5.6", "Low", "Both", false);
		}

		public async Task onDatabaseDataRetrieved(DatabaseDataRetrievedEventArgs e)
		{
			// If there was no aggregate in the query (it is many rows of unaggregated data
			// Upload it to the file library for analysis, otherwise just pass it through
			if (!e.SqlAnalysis.HasAggregate)
			{
				await pcw.SetCIDisposeOfFilesFlag(true);
				if (pcw.InvokeRequired)
				{
					await pcw.InvokeAsync(
						async cancellationToken =>
						{
							await pcw.SetToDataSetAsync(
								e.Data,
								true,
								true,
								-1);
						});
				}
				else
				{
					await pcw.SetToDataSetAsync(
						e.Data,
						true,
						true,
						-1);
				}
			}
		}

		private async void rbGeneral_CheckedChanged(object sender, EventArgs e)
		{
			if (rbGeneral.Checked)
			{
				// Reset the query engine so that prompts are not automtically routed through the SQL Query Engine
				await pcw.ResetQueryEngineAsync();

				// Clear the Code Interpreter of its managed file Ids. This does not delete uploaded files
				await pcw.ClearCITool();
			}
		}

		private async void rbQueryEphemeral_CheckedChanged(object sender, EventArgs e)
		{
			if (rbQueryEphemeral.Checked)
			{
				// Reset the query engine so that prompts are not automtically routed through the SQL Query Engine
				await pcw.ResetQueryEngineAsync();

				// Update the code interpreter to manage uploaded ephemeral files
				await pcw.UpdateCIToolToIncludeEphemeralFiles();
			}
		}

		private async void rbQueryJson_CheckedChanged(object sender, EventArgs e)
		{
			if (rbQueryJson.Checked)
			{
				// Reset the query engine so that prompts are not automtically routed through the SQL Query Engine
				await pcw.ResetQueryEngineAsync();

				// Update the code interpreter to manage uploaded json files created through 
				await pcw.UpdateCIToolToIncludeJSONFiles();
			}
		}

		private async void rbQueryDB_CheckedChanged(object sender, EventArgs e)
		{
			if (rbQueryDB.Checked)
			{
				string ai = "When asked for delimited records, return only the raw delimited records without any futher conversation." +
					"Do not include any html in the answer or any other data beyond the deimited records being asked for. Do not surround answer with other text. " +
					"Do not return the generated sql.";

				// Pass along additional instructions
				pcw.SetAdditionalInstructions(ai);

				// Set the query enegine with a connection string of the database to be analyzed. Data queries
				// will be routed through the query engine
				await pcw.InitializeQueryEngineAsync(contosoConnectionString);
			}
		}

		private async void btnSend_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(txtPrompt.Text))
			{
				// Send a prompt to the assistant programmatically. This applies to UI and headless modes
				await pcw.SendRemotePromptAsync("Dynamic Tab", txtPrompt.Text, "gpt-5.6", "Low", "Both", false);
			}
		}
	}
}

