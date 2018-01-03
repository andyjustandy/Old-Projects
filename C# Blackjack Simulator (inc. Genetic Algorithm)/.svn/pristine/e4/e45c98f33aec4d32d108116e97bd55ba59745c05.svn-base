using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;

namespace GeneticBlackjackSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Stores the current Game Simulation Input.
        private GameSimulationInput _GAME_SIMULATION_INPUT;

        // Stores the current Evolution Simulation Input.
        private EvolutionSimulationInput _EVOLUTION_SIMULATION_INPUT;

        /* The main window will Initialise then populate the Player Strategy dategrid with the default strategy. 
         * Then populate the Dealer Strategy datagrid with the default dealer strategy.
         * Finally Prepares the Breeding Pool view model by creating a new StrategyViewModel and sets the properties to the view.
         * ***/
        public MainWindow()
        {
            InitializeComponent();

            // Initial load of the player and dealer strategies, into the Game Input data grids.
            string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            string playerDefault = startupPath + @"\Strategies\DefaultStandStrategy.xml";
            string dealerDefault = startupPath + @"\Strategies\DealerStandsOnSoft17.xml";
            PopulatePlayerGridWithStrategy(playerDefault);
            PopulateDealerGridWithStrategy(dealerDefault);
            PrepareStrategyViewModel();
        }

        #region - GAME SIMULATION -
        // Simulates a Blackjack game using the game simulation user input.
        private void btnSimulateGame_Click(object sender, RoutedEventArgs e)
        {
            // Sets all of the Game Simulation Input parameters
            PlayerStrategy _player_strategy = new PlayerStrategy(GetPlayerStrategyNameInput(), DataGridMergeToTable(dataGrid_Player_Hard_Soft, dataGrid_Player_Pair));       // Sets the _player_strategy parameter to the player strategy in the datagrid_player.
            decimal _balance = Convert.ToInt32(txtbPlayerBalanceSlider.Text);                                  // Sets the _balance parameter to the balance input text.
            decimal _bet_amount = Convert.ToInt32(txtbPlayerFlatBetSlider.Text);                               // Sets the _bet_amount parameter to the flatbet input text.
            DealerStrategy _dealer_strategy = new DealerStrategy(GetDealerStrategyNameInput(), DataGridToTable(dataGrid_Dealer));        // Sets the _dealer_strategy parameter to the dealer strategy in the datagrid_dealer.
            int _number_of_decks = GetNumberOfDecksInput();                                                // Sets the _number_of_decks parameter to the selected number of decks radio button.
            int _deal_percentage = Convert.ToInt32(txtbPercentageSlider.Text);                             // Sets the _deal_percentage parameter to the number percentage in the textbox (slider).
            int _number_of_simulation_hands = Convert.ToInt32(txtbSimulationHandsSlider.Text);
            GameSimulationInput.EnumDoubleOn _double_on = GetDoubleOnOptionInput();                        // Sets the _double_on parameter to the selected double on radio button.
            bool _double_after_split = GetDoubleAfterSplitCBInput();                                       // Sets the _double_after_split parameter to the selected check box input.
            GameSimulationInput.EnumSurrender _surrender_option = GetSurrenderOptionInput();               // Sets the _surrender_option parameter to the selected surrender radio button input.
            int _number_of_replit_option = GetNumberOFResplitsFromOption();                                // Sets the _number_of_replit_option parameter to the selected resplit radio button input.
            bool _cannot_split_aces = GetCannotSplitAcesCBInput();                                         // Sets the _cannot_split_aces parameter to the selected check box input.
            bool _hit_split_aces = GetHitSplitAcesCBInput();                                               // Sets the _hit_split_aces parameter to the selected check box input.
            bool _cannot_split_4s_5s_10s = GetCannotHitSplit4s5s10sCBInput();                              // Sets the _cannot_split_4s_5s_10s parameter to the selected check box input.
            bool _shuffle_after_each_hand = GetSuffleAfterEachHandCBInput();                               // Sets the _shuffle_after_each_hand parameter to the selected check box input.
            GameSimulationInput.EnumCardBonus _card_bonus_option = GetCardBonusOptionInput();              // Sets the _card_bonus_option parameter to the selected card bonus radio button input.
            bool _lucky_777 = GetLucky777CBInput();                                                        // Sets the _lucky_777 parameter to the selected check box input.
            GameSimulationInput.EnumBlackJackPays _bj_pays_option = GetBlackjackPaysOptionInput();         // Sets the _bj_pays_option parameter to the selected Backjack paying radio button input.
            bool _suited_bj_pays_2to1 = GetSuitedBJPays2to1CBInput();                                      // Sets the _suited_bj_pays_2to1 parameter to the selected check box input.

            // Creates a new GameSimulationInput object in preparation for the game simulation.
            _GAME_SIMULATION_INPUT = new GameSimulationInput(_player_strategy, _balance, _bet_amount, _dealer_strategy,
            _number_of_decks, _deal_percentage, _number_of_simulation_hands, _double_on, _double_after_split,
            _surrender_option, _number_of_replit_option, _cannot_split_aces, _hit_split_aces,
            _cannot_split_4s_5s_10s, _shuffle_after_each_hand, _card_bonus_option,
            _lucky_777, _bj_pays_option, _suited_bj_pays_2to1);

            // Runs the Blackjack game simulation on
            GameSimulation.RunGameSimulation(this.txtb_ReportView, _GAME_SIMULATION_INPUT, false);

            // Selects the next tab (the only way of accessing the Genetic Algorithm input tab).
            tabControl.SelectedIndex = (tabControl.SelectedIndex + 1);  
        }
        #endregion

        #region - PLAYER STRATEGY GRID -
        // Populates the PlayerGrid with a strategy (XML File -> DataTable -> DataGrid) 
        public void PopulatePlayerGridWithStrategy(string filepath)
        {
            // Create data table
            DataTable PlayerDataTable = new DataTable();

            // Create 10 columns with the column headers (2,3,4,5,6,7,8,9,10,A)
            PlayerDataTable.Columns.Add("2");
            PlayerDataTable.Columns.Add("3");
            PlayerDataTable.Columns.Add("4");
            PlayerDataTable.Columns.Add("5");
            PlayerDataTable.Columns.Add("6");
            PlayerDataTable.Columns.Add("7");
            PlayerDataTable.Columns.Add("8");
            PlayerDataTable.Columns.Add("9");
            PlayerDataTable.Columns.Add("10");
            PlayerDataTable.Columns.Add("A");

            // Modifies the Strategy datagrids in run time, expanding the cells to "*".
            dataGrid_Player_Hard_Soft.AutoGeneratingColumn += (s, e) =>
            {
                e.Column.Width = new DataGridLength(1,
                                  DataGridLengthUnitType.Star
                                  );
            };
            dataGrid_Player_Pair.AutoGeneratingColumn += (s, e) =>
            {
                e.Column.Width = new DataGridLength(1,
                                  DataGridLengthUnitType.Star
                                  );
            };

            // The whole strategy datatable loaded from an xml file.
            PlayerDataTable = LoadDataPlayerTableFromXML(filepath); 

            DataTable player_hard_soft_datatable = new DataTable();
            // Create 10 columns with the column headers (2,3,4,5,6,7,8,9,10,A)
            player_hard_soft_datatable.Columns.Add("2");
            player_hard_soft_datatable.Columns.Add("3");
            player_hard_soft_datatable.Columns.Add("4");
            player_hard_soft_datatable.Columns.Add("5");
            player_hard_soft_datatable.Columns.Add("6");
            player_hard_soft_datatable.Columns.Add("7");
            player_hard_soft_datatable.Columns.Add("8");
            player_hard_soft_datatable.Columns.Add("9");
            player_hard_soft_datatable.Columns.Add("10");
            player_hard_soft_datatable.Columns.Add("A");
            DataTable player_pair_datatable = new DataTable();
            // Create 10 columns with the column headers (2,3,4,5,6,7,8,9,10,A)
            player_pair_datatable.Columns.Add("2");
            player_pair_datatable.Columns.Add("3");
            player_pair_datatable.Columns.Add("4");
            player_pair_datatable.Columns.Add("5");
            player_pair_datatable.Columns.Add("6");
            player_pair_datatable.Columns.Add("7");
            player_pair_datatable.Columns.Add("8");
            player_pair_datatable.Columns.Add("9");
            player_pair_datatable.Columns.Add("10");
            player_pair_datatable.Columns.Add("A");

            int rowIndex = 0;
            
            // Extracts the Data from the whole table into two tables. One for the Hard/Soft Player Strategy and the other Pairs Player Strategy.
            foreach (DataRow row in PlayerDataTable.Rows)
            {
                rowIndex++;

                if (rowIndex <= 26)
                {
                    player_hard_soft_datatable.ImportRow(row);
                }
                if (rowIndex > 26 && rowIndex <= 36)
                {
                    player_pair_datatable.ImportRow(row);
                }
            }

            // Sets the Datagrid (Player Strategy) to each of the tables created.
            dataGrid_Player_Hard_Soft.DataContext = player_hard_soft_datatable;
            dataGrid_Player_Pair.DataContext = player_pair_datatable;
        }

        // OPENS PLAYER STRATEGY: Converts the XML (player strategy) values from file to DataTable, and displays the DataTable in the datagrid.
        private void btnOpenPlayerGrid_Click(object sender, RoutedEventArgs e)
        {
            // Opens the open file dialog and narrows the view to only Xml documents.
            OpenFileDialog openfd = new OpenFileDialog() { Filter = "Xml Documents|*.xml", ValidateNames = true, Multiselect = false };
            
            // When the open button is clicked.
            if (openfd.ShowDialog() == true)
            {
                // Creates a new stream to retrieve the file data.
                StreamReader sr = new StreamReader(openfd.FileName);

                // Populates the grid from selected file. 
                PopulatePlayerGridWithStrategy(openfd.InitialDirectory + openfd.FileName);

                // Closes the stream reader.
                sr.Close();
            }  
        }

        // SAVES PLAYER STRATEGY: Converts the DataGrid (player strategy) values from DataTable to XML, and saves the XML in a strategy structure.
        private void btnSavePlayerGrid_Click(object sender, RoutedEventArgs e)
        {
            // Gets the data (Hard/Soft and Pair) strategies from the datagrids.
            DataTable dataTable_Hard_Soft = new DataTable();
            dataTable_Hard_Soft = ((DataView)dataGrid_Player_Hard_Soft.ItemsSource).ToTable();
            DataTable dataTable_Pair = new DataTable();
            dataTable_Pair = ((DataView)dataGrid_Player_Pair.ItemsSource).ToTable();
            
            // Opens the save file dialog and narrows the view to only xml documents.
            SaveFileDialog savefd = new SaveFileDialog() { Filter = "Xml Documents|*.xml", ValidateNames = true };

            // When save button clicked.
            if (savefd.ShowDialog() == true)
            {
                // Creates a list for the xml elements.
                List<XElement> elementList = new List<XElement>();

                // If the file already exists, the xml strategy will be written over the current document structure.
                if (File.Exists(savefd.FileName))
                {
                    XDocument xmlDoc = XDocument.Load(savefd.FileName);

                    #region - Writes the the existing structure of the xml -
                    XElement input = new XElement("player_strategy",
                        new XAttribute("Name", "Basic Strategy"),
                        new XAttribute("Date", DateTime.Today.Date.ToShortDateString()),
                        new XElement("Hard5",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[0][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[0][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[0][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[0][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[0][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[0][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[0][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[0][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[0][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[0][9])),
                        new XElement("Hard6",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[1][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[1][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[1][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[1][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[1][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[1][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[1][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[1][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[1][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[1][9])),
                        new XElement("Hard7",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[2][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[2][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[2][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[2][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[2][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[2][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[2][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[2][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[2][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[2][9])),
                        new XElement("Hard8",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[3][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[3][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[3][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[3][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[3][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[3][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[3][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[3][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[3][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[3][9])),
                        new XElement("Hard9",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[4][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[4][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[4][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[4][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[4][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[4][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[4][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[4][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[4][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[4][9])),
                        new XElement("Hard10",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[5][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[5][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[5][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[5][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[5][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[5][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[5][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[5][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[5][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[5][9])),
                        new XElement("Hard11",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[6][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[6][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[6][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[6][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[6][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[6][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[6][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[6][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[6][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[6][9])),
                        new XElement("Hard12",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[7][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[7][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[7][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[7][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[7][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[7][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[7][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[7][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[7][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[7][9])),
                        new XElement("Hard13",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[8][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[8][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[8][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[8][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[8][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[8][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[8][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[8][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[8][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[8][9])),
                        new XElement("Hard14",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[9][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[9][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[9][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[9][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[9][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[9][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[9][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[9][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[9][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[9][9])),
                        new XElement("Hard15",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[10][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[10][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[10][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[10][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[10][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[10][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[10][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[10][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[10][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[10][9])),
                        new XElement("Hard16",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[11][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[11][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[11][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[11][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[11][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[11][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[11][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[11][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[11][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[11][9])),
                        new XElement("Hard17",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[12][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[12][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[12][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[12][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[12][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[12][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[12][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[12][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[12][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[12][9])),
                        new XElement("Hard18",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[13][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[13][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[13][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[13][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[13][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[13][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[13][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[13][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[13][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[13][9])),
                        new XElement("Hard19",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[14][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[14][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[14][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[14][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[14][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[14][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[14][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[14][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[14][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[14][9])),
                        new XElement("Hard20",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[15][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[15][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[15][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[15][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[15][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[15][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[15][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[15][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[15][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[15][9])),
                        new XElement("Hard21",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[16][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[16][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[16][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[16][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[16][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[16][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[16][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[16][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[16][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[16][9])),
                        new XElement("Soft13",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[17][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[17][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[17][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[17][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[17][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[17][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[17][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[17][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[17][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[17][9])),
                        new XElement("Soft14",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[18][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[18][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[18][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[18][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[18][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[18][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[18][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[18][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[18][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[18][9])),
                        new XElement("Soft15",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[19][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[19][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[19][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[19][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[19][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[19][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[19][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[19][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[19][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[19][9])),
                        new XElement("Soft16",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[20][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[20][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[20][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[20][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[20][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[20][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[20][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[20][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[20][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[20][9])),
                        new XElement("Soft17",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[21][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[21][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[21][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[21][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[21][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[21][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[21][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[21][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[21][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[21][9])),
                        new XElement("Soft18",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[22][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[22][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[22][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[22][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[22][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[22][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[22][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[22][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[22][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[22][9])),
                        new XElement("Soft19",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[23][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[23][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[23][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[23][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[23][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[23][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[23][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[23][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[23][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[23][9])),
                        new XElement("Soft20",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[24][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[24][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[24][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[24][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[24][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[24][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[24][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[24][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[24][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[24][9])),
                        new XElement("Soft21",
                            new XElement("Dealer2", dataTable_Hard_Soft.Rows[25][0]),
                            new XElement("Dealer3", dataTable_Hard_Soft.Rows[25][1]),
                            new XElement("Dealer4", dataTable_Hard_Soft.Rows[25][2]),
                            new XElement("Dealer5", dataTable_Hard_Soft.Rows[25][3]),
                            new XElement("Dealer6", dataTable_Hard_Soft.Rows[25][4]),
                            new XElement("Dealer7", dataTable_Hard_Soft.Rows[25][5]),
                            new XElement("Dealer8", dataTable_Hard_Soft.Rows[25][6]),
                            new XElement("Dealer9", dataTable_Hard_Soft.Rows[25][7]),
                            new XElement("Dealer10", dataTable_Hard_Soft.Rows[25][8]),
                            new XElement("DealerA", dataTable_Hard_Soft.Rows[25][9])),
                        new XElement("Pair2",
                            new XElement("Dealer2", dataTable_Pair.Rows[0][0]),
                            new XElement("Dealer3", dataTable_Pair.Rows[0][1]),
                            new XElement("Dealer4", dataTable_Pair.Rows[0][2]),
                            new XElement("Dealer5", dataTable_Pair.Rows[0][3]),
                            new XElement("Dealer6", dataTable_Pair.Rows[0][4]),
                            new XElement("Dealer7", dataTable_Pair.Rows[0][5]),
                            new XElement("Dealer8", dataTable_Pair.Rows[0][6]),
                            new XElement("Dealer9", dataTable_Pair.Rows[0][7]),
                            new XElement("Dealer10", dataTable_Pair.Rows[0][8]),
                            new XElement("DealerA", dataTable_Pair.Rows[0][9])),
                        new XElement("Pair3",
                            new XElement("Dealer2", dataTable_Pair.Rows[1][0]),
                            new XElement("Dealer3", dataTable_Pair.Rows[1][1]),
                            new XElement("Dealer4", dataTable_Pair.Rows[1][2]),
                            new XElement("Dealer5", dataTable_Pair.Rows[1][3]),
                            new XElement("Dealer6", dataTable_Pair.Rows[1][4]),
                            new XElement("Dealer7", dataTable_Pair.Rows[1][5]),
                            new XElement("Dealer8", dataTable_Pair.Rows[1][6]),
                            new XElement("Dealer9", dataTable_Pair.Rows[1][7]),
                            new XElement("Dealer10", dataTable_Pair.Rows[1][8]),
                            new XElement("DealerA", dataTable_Pair.Rows[1][9])),
                        new XElement("Pair4",
                            new XElement("Dealer2", dataTable_Pair.Rows[2][0]),
                            new XElement("Dealer3", dataTable_Pair.Rows[2][1]),
                            new XElement("Dealer4", dataTable_Pair.Rows[2][2]),
                            new XElement("Dealer5", dataTable_Pair.Rows[2][3]),
                            new XElement("Dealer6", dataTable_Pair.Rows[2][4]),
                            new XElement("Dealer7", dataTable_Pair.Rows[2][5]),
                            new XElement("Dealer8", dataTable_Pair.Rows[2][6]),
                            new XElement("Dealer9", dataTable_Pair.Rows[2][7]),
                            new XElement("Dealer10", dataTable_Pair.Rows[2][8]),
                            new XElement("DealerA", dataTable_Pair.Rows[2][9])),
                        new XElement("Pair5",
                            new XElement("Dealer2", dataTable_Pair.Rows[3][0]),
                            new XElement("Dealer3", dataTable_Pair.Rows[3][1]),
                            new XElement("Dealer4", dataTable_Pair.Rows[3][2]),
                            new XElement("Dealer5", dataTable_Pair.Rows[3][3]),
                            new XElement("Dealer6", dataTable_Pair.Rows[3][4]),
                            new XElement("Dealer7", dataTable_Pair.Rows[3][5]),
                            new XElement("Dealer8", dataTable_Pair.Rows[3][6]),
                            new XElement("Dealer9", dataTable_Pair.Rows[3][7]),
                            new XElement("Dealer10", dataTable_Pair.Rows[3][8]),
                            new XElement("DealerA", dataTable_Pair.Rows[3][9])),
                        new XElement("Pair6",
                            new XElement("Dealer2", dataTable_Pair.Rows[4][0]),
                            new XElement("Dealer3", dataTable_Pair.Rows[4][1]),
                            new XElement("Dealer4", dataTable_Pair.Rows[4][2]),
                            new XElement("Dealer5", dataTable_Pair.Rows[4][3]),
                            new XElement("Dealer6", dataTable_Pair.Rows[4][4]),
                            new XElement("Dealer7", dataTable_Pair.Rows[4][5]),
                            new XElement("Dealer8", dataTable_Pair.Rows[4][6]),
                            new XElement("Dealer9", dataTable_Pair.Rows[4][7]),
                            new XElement("Dealer10", dataTable_Pair.Rows[4][8]),
                            new XElement("DealerA", dataTable_Pair.Rows[4][9])),
                        new XElement("Pair7",
                            new XElement("Dealer2", dataTable_Pair.Rows[5][0]),
                            new XElement("Dealer3", dataTable_Pair.Rows[5][1]),
                            new XElement("Dealer4", dataTable_Pair.Rows[5][2]),
                            new XElement("Dealer5", dataTable_Pair.Rows[5][3]),
                            new XElement("Dealer6", dataTable_Pair.Rows[5][4]),
                            new XElement("Dealer7", dataTable_Pair.Rows[5][5]),
                            new XElement("Dealer8", dataTable_Pair.Rows[5][6]),
                            new XElement("Dealer9", dataTable_Pair.Rows[5][7]),
                            new XElement("Dealer10", dataTable_Pair.Rows[5][8]),
                            new XElement("DealerA", dataTable_Pair.Rows[5][9])),
                        new XElement("Pair8",
                            new XElement("Dealer2", dataTable_Pair.Rows[6][0]),
                            new XElement("Dealer3", dataTable_Pair.Rows[6][1]),
                            new XElement("Dealer4", dataTable_Pair.Rows[6][2]),
                            new XElement("Dealer5", dataTable_Pair.Rows[6][3]),
                            new XElement("Dealer6", dataTable_Pair.Rows[6][4]),
                            new XElement("Dealer7", dataTable_Pair.Rows[6][5]),
                            new XElement("Dealer8", dataTable_Pair.Rows[6][6]),
                            new XElement("Dealer9", dataTable_Pair.Rows[6][7]),
                            new XElement("Dealer10", dataTable_Pair.Rows[6][8]),
                            new XElement("DealerA", dataTable_Pair.Rows[6][9])),
                        new XElement("Pair9",
                            new XElement("Dealer2", dataTable_Pair.Rows[7][0]),
                            new XElement("Dealer3", dataTable_Pair.Rows[7][1]),
                            new XElement("Dealer4", dataTable_Pair.Rows[7][2]),
                            new XElement("Dealer5", dataTable_Pair.Rows[7][3]),
                            new XElement("Dealer6", dataTable_Pair.Rows[7][4]),
                            new XElement("Dealer7", dataTable_Pair.Rows[7][5]),
                            new XElement("Dealer8", dataTable_Pair.Rows[7][6]),
                            new XElement("Dealer9", dataTable_Pair.Rows[7][7]),
                            new XElement("Dealer10", dataTable_Pair.Rows[7][8]),
                            new XElement("DealerA", dataTable_Pair.Rows[7][9])),
                        new XElement("Pair10",
                            new XElement("Dealer2", dataTable_Pair.Rows[8][0]),
                            new XElement("Dealer3", dataTable_Pair.Rows[8][1]),
                            new XElement("Dealer4", dataTable_Pair.Rows[8][2]),
                            new XElement("Dealer5", dataTable_Pair.Rows[8][3]),
                            new XElement("Dealer6", dataTable_Pair.Rows[8][4]),
                            new XElement("Dealer7", dataTable_Pair.Rows[8][5]),
                            new XElement("Dealer8", dataTable_Pair.Rows[8][6]),
                            new XElement("Dealer9", dataTable_Pair.Rows[8][7]),
                            new XElement("Dealer10", dataTable_Pair.Rows[8][8]),
                            new XElement("DealerA", dataTable_Pair.Rows[8][9])),
                        new XElement("PairA",
                            new XElement("Dealer2", dataTable_Pair.Rows[9][0]),
                            new XElement("Dealer3", dataTable_Pair.Rows[9][1]),
                            new XElement("Dealer4", dataTable_Pair.Rows[9][2]),
                            new XElement("Dealer5", dataTable_Pair.Rows[9][3]),
                            new XElement("Dealer6", dataTable_Pair.Rows[9][4]),
                            new XElement("Dealer7", dataTable_Pair.Rows[9][5]),
                            new XElement("Dealer8", dataTable_Pair.Rows[9][6]),
                            new XElement("Dealer9", dataTable_Pair.Rows[9][7]),
                            new XElement("Dealer10", dataTable_Pair.Rows[9][8]),
                            new XElement("DealerA", dataTable_Pair.Rows[9][9]))
                    );
                    #endregion

                    xmlDoc.Root.Add(input);

                    // Saves over an existing document.
                    xmlDoc.Save(savefd.FileName);
                }
                // Otherwise if the file does not exist, the pre defined format structure will be created with the xml strategy.
                else
                {
                    // Writes the to file using an XML writer and creates a new xml document.
                    using (XmlWriter writer = XmlWriter.Create(savefd.FileName))
                    {
                        writer.WriteStartDocument();
                        writer.WriteComment("This log was created by the Application.");
                        writer.WriteStartElement("input");
                        writer.WriteStartElement("player_strategy");
                        writer.WriteAttributeString("Name", "Basic Strategy");
                        writer.WriteAttributeString("Date", DateTime.Today.Date.ToShortDateString());

                        #region - Writes the the existing structure of the xml -
                        DataTable dataTable = new DataTable();
                        dataTable = DataGridMergeToTable(dataGrid_Player_Hard_Soft, dataGrid_Player_Pair);
                        int rowHealerIndex = 2;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (rowHealerIndex <= 17)
                            {
                                writer.WriteStartElement("Hard" + (rowHealerIndex + 3));
                                writer.WriteElementString("Dealer2", row[0].ToString());
                                writer.WriteElementString("Dealer3", row[1].ToString());
                                writer.WriteElementString("Dealer4", row[2].ToString());
                                writer.WriteElementString("Dealer5", row[3].ToString());
                                writer.WriteElementString("Dealer6", row[4].ToString());
                                writer.WriteElementString("Dealer7", row[5].ToString());
                                writer.WriteElementString("Dealer8", row[6].ToString());
                                writer.WriteElementString("Dealer9", row[7].ToString());
                                writer.WriteElementString("Dealer10", row[8].ToString());
                                writer.WriteElementString("DealerA", row[9].ToString());
                                writer.WriteEndElement();
                            }
                            if (rowHealerIndex > 17 && rowHealerIndex <= 26)
                            {
                                writer.WriteStartElement("Soft" + (rowHealerIndex - 6));
                                writer.WriteElementString("Dealer2", row[0].ToString());
                                writer.WriteElementString("Dealer3", row[1].ToString());
                                writer.WriteElementString("Dealer4", row[2].ToString());
                                writer.WriteElementString("Dealer5", row[3].ToString());
                                writer.WriteElementString("Dealer6", row[4].ToString());
                                writer.WriteElementString("Dealer7", row[5].ToString());
                                writer.WriteElementString("Dealer8", row[6].ToString());
                                writer.WriteElementString("Dealer9", row[7].ToString());
                                writer.WriteElementString("Dealer10", row[8].ToString());
                                writer.WriteElementString("DealerA", row[9].ToString());
                                writer.WriteEndElement();
                            }
                            if (rowHealerIndex > 26)
                            {
                                writer.WriteStartElement("Pair" + (rowHealerIndex - 25));
                                writer.WriteElementString("Dealer2", row[0].ToString());
                                writer.WriteElementString("Dealer3", row[1].ToString());
                                writer.WriteElementString("Dealer4", row[2].ToString());
                                writer.WriteElementString("Dealer5", row[3].ToString());
                                writer.WriteElementString("Dealer6", row[4].ToString());
                                writer.WriteElementString("Dealer7", row[5].ToString());
                                writer.WriteElementString("Dealer8", row[6].ToString());
                                writer.WriteElementString("Dealer9", row[7].ToString());
                                writer.WriteElementString("Dealer10", row[8].ToString());
                                writer.WriteElementString("DealerA", row[9].ToString());
                                writer.WriteEndElement();
                                
                                // If the index is the last row, create the "PairA" header. 
                                if (rowHealerIndex == 38)
                                {
                                    writer.WriteStartElement("PairA");
                                    writer.WriteElementString("Dealer2", row[0].ToString());
                                    writer.WriteElementString("Dealer3", row[1].ToString());
                                    writer.WriteElementString("Dealer4", row[2].ToString());
                                    writer.WriteElementString("Dealer5", row[3].ToString());
                                    writer.WriteElementString("Dealer6", row[4].ToString());
                                    writer.WriteElementString("Dealer7", row[5].ToString());
                                    writer.WriteElementString("Dealer8", row[6].ToString());
                                    writer.WriteElementString("Dealer9", row[7].ToString());
                                    writer.WriteElementString("Dealer10", row[8].ToString());
                                    writer.WriteElementString("DealerA", row[9].ToString());
                                    writer.WriteEndElement();
                                }
                            }
                            rowHealerIndex++;
                        }
                        #endregion

                        // Finishes the element.
                        writer.WriteEndElement();

                        // Flush from the buffer to the steam.
                        writer.Flush();
                        
                        // Writes to a new xml document.
                        writer.WriteEndDocument();
                    }
                }
                // Shows the file has been saved.
                MessageBox.Show("You have been successfully saved.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Player Hard Soft Strategy datagrid input validation is handled here.
        private void dataGrid_Player_Hard_Soft_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // If the user presses an arrow key
            if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Enter)
            {
                // Arrows Key pressed will be allowed.
            }
            else
            {
                bool allowedKeys;
                bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;

                // If CapsLock is not ON, then turn CapsLock ON and block the entry.
                if (!CapsLock)
                {
                    allowedKeys = true;
                    keybd_event(CAPSLOCK, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                    keybd_event(CAPSLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
                }
                // When CapsLock is on, and the input character is equal to one of the Player Actions. Then allow the character entry.
                else if (e.Key.ToString() == "S" || e.Key.ToString() == "H" || e.Key.ToString() == "D" || e.Key.ToString() == "R")
                {
                    // Clears the cell before entering a new player action into the datagrid
                    DataGridCellInfo cell = dataGrid_Player_Hard_Soft.SelectedCells[dataGrid_Player_Hard_Soft.CurrentCell.Column.DisplayIndex];
                    try
                    {
                        ((TextBlock)cell.Column.GetCellContent(cell.Item)).Text = "";
                    }
                    catch
                    {
                        ((TextBox)cell.Column.GetCellContent(cell.Item)).Text = "";
                    }
                    allowedKeys = false;
                }
                // If the input character is not a valid Player Action, block the entry.
                else allowedKeys = true;

                e.Handled = allowedKeys;
            }
        }

        // Player Pair Strategy datagrid input validation is handled here.
        private void dataGrid_Player_Pair_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // If the user presses an arrow key
            if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Enter)
            {
                // Arrows Key pressed will be allowed.
            }
            else
            {
                bool allowedKeys;
                bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;

                // If CapsLock is not ON, then turn CapsLock ON and block the entry.
                if (!CapsLock)
                {
                    allowedKeys = true;
                    keybd_event(CAPSLOCK, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                    keybd_event(CAPSLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
                }
                // When CapsLock is on, and the input character is equal to one of the Player Actions. Then allow the character entry.
                else if (e.Key.ToString() == "S" || e.Key.ToString() == "H" || e.Key.ToString() == "P" || e.Key.ToString() == "D" || e.Key.ToString() == "R")
                {
                    // Clears the cell before entering a new player action into the datagrid
                    DataGridCellInfo cell = dataGrid_Player_Pair.SelectedCells[dataGrid_Player_Pair.CurrentCell.Column.DisplayIndex];
                    try
                    {
                        ((TextBlock)cell.Column.GetCellContent(cell.Item)).Text = "";
                    }
                    catch
                    {
                        ((TextBox)cell.Column.GetCellContent(cell.Item)).Text = "";

                    }
                    allowedKeys = false;
                }
                // If the input character is not a valid Player Action, block the entry.
                else allowedKeys = true;
                e.Handled = allowedKeys;
            }
        }
        #endregion

        #region - DEALER STRATEGY GRID -
        // Populates the DealerGrid with a strategy (XML File -> DataTable -> DataGrid) 
        public void PopulateDealerGridWithStrategy(string filepath)
        {
            // Create data table
            DataTable _myDataTable = new DataTable();

            // Create the column for the dealer's strategy actions with the column header (____)
            _myDataTable.Columns.Add("____");

            // Modifies the Strategy datagrid at run time, expanding the cells to "*".
            dataGrid_Dealer.AutoGeneratingColumn += (s, e) =>
            {
                e.Column.Width = new DataGridLength(1,
                                  DataGridLengthUnitType.Star
                                  );
            };

            // Loads the DataTable from the xml file and sets the DataGrid View to the strategy.
            dataGrid_Dealer.DataContext = LoadDataDealerTableFromXML(filepath);
        }

        // OPENS DEALER STRATEGY: Converts the XML (dealer strategy) values from file to DataTable, and displays the DataTable in the datagrid.
        private void btnOpenDealerGrid_Click(object sender, RoutedEventArgs e)
        {
            // Opens the open file dialog and narrows the view to only Xml documents.
            OpenFileDialog openfd = new OpenFileDialog() { Filter = "Xml Documents|*.xml", ValidateNames = true, Multiselect = false };

            // When the open button is clicked.
            if (openfd.ShowDialog() == true)
            {
                // Creates a new stream to retrieve the file data.
                StreamReader sr = new StreamReader(openfd.FileName);

                // Populates the grid from selected file. 
                PopulateDealerGridWithStrategy(openfd.InitialDirectory + openfd.FileName);

                // Closes the stream reader.
                sr.Close();
            }
        }

        // SAVES DEALER STRATEGY: Converts the DataGrid (dealer strategy) values from DataTable to XML, and saves the XML in a strategy structure.
        private void btnSaveDealerGrid_Click(object sender, RoutedEventArgs e)
        {
            // Gets the data (dealer strategy) from the datagrids.
            DataTable dataTable = new DataTable();
            dataTable = ((DataView)dataGrid_Dealer.ItemsSource).ToTable();

            // Opens the save file dialog and narrows the view to only xml documents.
            SaveFileDialog savefd = new SaveFileDialog() { Filter = "Xml Documents|*.xml", ValidateNames = true };
            
            // When save button clicked.
            if (savefd.ShowDialog() == true)
            {
                // Creates a list for the xml elements.
                List<XElement> elementList = new List<XElement>();

                // If the file already exists, the xml strategy will be written over the current document structure.
                if (File.Exists(savefd.FileName))
                {
                    // Loads the xml document into a local variable.
                    XDocument xmlDoc = XDocument.Load(savefd.FileName);

                    #region - Writes the the existing structure of the xml -
                    XElement input = new XElement("dealer_strategy",
                        new XAttribute("Name", "Basic Strategy"),
                        new XAttribute("Date", DateTime.Today.Date.ToShortDateString()),
                        new XElement("Hard5", dataTable.Rows[0][0]),
                        new XElement("Hard6", dataTable.Rows[1][0]),
                        new XElement("Hard7", dataTable.Rows[2][0]),
                        new XElement("Hard8", dataTable.Rows[3][0]),
                        new XElement("Hard9", dataTable.Rows[4][0]),
                        new XElement("Hard10", dataTable.Rows[5][0]),
                        new XElement("Hard11", dataTable.Rows[6][0]),
                        new XElement("Hard12", dataTable.Rows[7][0]),
                        new XElement("Hard13", dataTable.Rows[8][0]),
                        new XElement("Hard14", dataTable.Rows[9][0]),
                        new XElement("Hard15", dataTable.Rows[10][0]),
                        new XElement("Hard16", dataTable.Rows[11][0]),
                        new XElement("Hard17", dataTable.Rows[12][0]),
                        new XElement("Hard18", dataTable.Rows[13][0]),
                        new XElement("Hard19", dataTable.Rows[14][0]),
                        new XElement("Hard20", dataTable.Rows[15][0]),
                        new XElement("Hard21", dataTable.Rows[16][0]),
                        new XElement("Soft13", dataTable.Rows[17][0]),
                        new XElement("Soft14", dataTable.Rows[18][0]),
                        new XElement("Soft15", dataTable.Rows[19][0]),
                        new XElement("Soft16", dataTable.Rows[20][0]),
                        new XElement("Soft17", dataTable.Rows[21][0]),
                        new XElement("Soft18", dataTable.Rows[22][0]),
                        new XElement("Soft19", dataTable.Rows[23][0]),
                        new XElement("Soft20", dataTable.Rows[24][0]),
                        new XElement("Soft21", dataTable.Rows[25][0]),
                        new XElement("Pair2", dataTable.Rows[26][0]),
                        new XElement("Pair3", dataTable.Rows[27][0]),
                        new XElement("Pair4", dataTable.Rows[28][0]),
                        new XElement("Pair5", dataTable.Rows[29][0]),
                        new XElement("Pair6", dataTable.Rows[30][0]),
                        new XElement("Pair7", dataTable.Rows[31][0]),
                        new XElement("Pair8", dataTable.Rows[32][0]),
                        new XElement("Pair9", dataTable.Rows[33][0]),
                        new XElement("Pair10", dataTable.Rows[34][0]),
                        new XElement("PairA", dataTable.Rows[35][0])
                    );
                    #endregion

                    xmlDoc.Root.Add(input);
                    xmlDoc.Save(savefd.FileName);
                }
                // Otherwise if the file does not exist, the pre defined format structure will be created with the xml strategy.
                else
                {
                    // Writes the to file using an XML writer and creates a new xml document.
                    using (XmlWriter writer = XmlWriter.Create(savefd.FileName))
                    {
                        writer.WriteStartDocument();
                        writer.WriteComment("This log was created by the Application.");
                        writer.WriteStartElement("input");
                        writer.WriteStartElement("dealer_strategy");
                        writer.WriteAttributeString("Name", "Basic Strategy");
                        writer.WriteAttributeString("Date", DateTime.Today.Date.ToShortDateString());

                        #region - Creates the structured format for the xml -
                        int index = 2;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (index <= 18)
                            {
                                writer.WriteElementString("Hard" + (index + 3), row[0].ToString());
                            }
                            if (index > 18 && index <= 27)
                            {
                                writer.WriteElementString("Soft" + (index - 6), row[0].ToString());
                            }
                            if (index > 27)
                            {
                                writer.WriteElementString("Pair" + (index - 26), row[0].ToString());

                                if (index == 38)
                                {
                                    writer.WriteStartElement("PairA", row[0].ToString());
                                }
                            }
                            index++;
                        }
                        #endregion

                        // Finishes the element.
                        writer.WriteEndElement();

                        // Flush from the buffer to the steam.
                        writer.Flush();

                        // Writes to a new xml document.
                        writer.WriteEndDocument();
                    }
                }
                MessageBox.Show("You have been successfully saved.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Dealer Strategy datagrid input validation is handled here.
        private void dataGrid_Dealer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // If the user presses an arrow key
            if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Enter)
            {
                // Arrows Key pressed will be allowed.
            }
            else
            {
                bool allowedKeys;
                bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;

                // If CapsLock is not ON, then turn CapsLock ON and block the entry.
                if (!CapsLock)
                {
                    allowedKeys = true;
                    keybd_event(CAPSLOCK, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                    keybd_event(CAPSLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
                }
                // When CapsLock is on, and the input character is equal to one of the Dealer Actions. Then allow the character entry.
                else if (e.Key.ToString() == "S" || e.Key.ToString() == "H")
                {
                    // Clears the cell before entering a new Dealer action into the datagrid
                    DataGridCellInfo cell = dataGrid_Dealer.SelectedCells[dataGrid_Dealer.CurrentCell.Column.DisplayIndex];
                    try
                    {
                        ((TextBlock)cell.Column.GetCellContent(cell.Item)).Text = "";
                    }
                    catch
                    {
                        ((TextBox)cell.Column.GetCellContent(cell.Item)).Text = "";

                    }
                    allowedKeys = false;
                }
                // If the input character is not a valid Dealer Action, block the entry.
                else allowedKeys = true;

                e.Handled = allowedKeys;
            }
        }
        #endregion

        #region - STRATEGY GRID - 
        // Loads the XML into the datagrid
        public static DataTable LoadDataPlayerTableFromXML(string pathname)
        {
            // Create a local datatable and xml document
            DataTable dataTable = new DataTable();
            XmlDocument xmlDoc = new XmlDocument();
            
            // Trys to set the local xmlDoc to the reader (loaded xml from file).
            try
            {
                using (TextReader reader = File.OpenText(pathname))
                {
                    xmlDoc.Load(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            XmlNodeList inputsXml = null;
            try
            {
                inputsXml = xmlDoc.SelectNodes("input")[0].ChildNodes;
            }
            catch
            {
                // If the input xml field does not exist the exception will be thrown for debugging.
                throw new ArgumentNullException("Missing <input> field\nProgram stopped.");
            }
            try
            {
                // Iterates through each starting element in the document.
                foreach (XmlNode inputXml in inputsXml)
                {
                    switch (inputXml.Name)
                    {
                        // PLAYER STRATEGY INPUT
                        case "player_strategy":
                            {
                                XmlDocument doc = new XmlDocument();  
                                doc.Load(pathname);

                                XmlNodeList xmlStrategyRowList = doc.GetElementsByTagName("player_strategy");
                                XmlNode xmlStrategyRowElement = xmlStrategyRowList[0];
                                XmlNodeList xmlRowList = xmlStrategyRowElement.ChildNodes;

                                // Creates the data table to parse the xml strategy into it
                                dataTable.Columns.Add("2");
                                dataTable.Columns.Add("3");
                                dataTable.Columns.Add("4");
                                dataTable.Columns.Add("5");
                                dataTable.Columns.Add("6");
                                dataTable.Columns.Add("7");
                                dataTable.Columns.Add("8");
                                dataTable.Columns.Add("9");
                                dataTable.Columns.Add("10");
                                dataTable.Columns.Add("A");

                                String[] nodeContent = new String[10];
                                foreach (XmlNode rowNode in xmlRowList)
                                {
                                    int nodeCount = 0;
                                    foreach (XmlNode columnNode in rowNode)
                                    {
                                        nodeContent[nodeCount] = columnNode.InnerText.ToString().Trim().ToUpper();
                                        nodeCount++;
                                    }
                                    dataTable.Rows.Add(nodeContent);
                                }

                                break;
                            }
                        // PLAYER BETTING STRATEGY INPUT
                        case "player_betting_strategy":
                            {
                                // TODO: IMPLEMENT BLACKJACK BETTING STRATEGY.
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }
            catch
            {
                // If there is no INPUT element case found, a message box will be displayed.
                MessageBox.Show("Missing Player XML <input_case> field\n-Please check the .XML file.","Strategy Loading Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            // Returns the loaded data table (strategy).
            return dataTable;
        }

        // Loads the XML into the datagrid
        public static DataTable LoadDataDealerTableFromXML(string pathname)
        {
            // Create a local datatable and xml document
            DataTable dataTable = new DataTable();  
            XmlDocument xmlDoc = new XmlDocument();

            // Trys to set the local xmlDoc to the reader (loaded xml from file).
            try
            {
                using (TextReader reader = File.OpenText(pathname))
                {
                    xmlDoc.Load(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            XmlNodeList inputsXml = null;
            try
            {
                inputsXml = xmlDoc.SelectNodes("input")[0].ChildNodes;
            }
            catch
            {
                // If the input xml field does not exist the exception will be thrown for debugging.
                throw new ArgumentNullException("Missing <input> field\nProgram stopped.");
            }
            try
            {
                // Iterates through each starting element in the document.
                foreach (XmlNode inputXml in inputsXml)
                {
                    switch (inputXml.Name)
                    {
                        // DEALER STRATEGY INPUT
                        case "dealer_strategy":
                            {
                                // Loads the dealerstrategy input.
                                XmlDocument doc = new XmlDocument();  
                                doc.Load(pathname);

                                XmlNodeList xmlStrategyRowList = doc.GetElementsByTagName("dealer_strategy");
                                XmlNode xmlStrategyRowElement = xmlStrategyRowList[0];
                                XmlNodeList xmlRowList = xmlStrategyRowElement.ChildNodes;

                                // Creates the data table column to parse the xml strategy into it.
                                dataTable.Columns.Add("____");
                                String nodeContent = "";
                                foreach (XmlNode rowNode in xmlRowList)
                                {
                                    nodeContent = rowNode.InnerText.ToString().Trim().ToUpper();

                                    dataTable.Rows.Add(nodeContent);
                                }
                            }
                            break;
                        default:
                            {
                                throw new Exception("Unknown Xml: " + inputXml.Name);
                            }
                    }
                }
            }
            catch
            {
                // Outputs an error message if there is no input case found.
                MessageBox.Show("Missing dealer XML <input_case> field\n-Please check the .XML file.", "Strategy Loading Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // Returns the loaded datatable (strategy).
            return dataTable;
        }

        // Loads the HARD, SOFT and PAIR row headers [36] to the strategy_datagrid.
        private void dataGrid_LoadingAllRowHeaders(object sender, DataGridRowEventArgs e)
        {
            var id = e.Row.GetIndex();
            switch (id)
            {
                case 0:
                    {
                        e.Row.Header = "Hard5";
                        break;
                    }
                case 1:
                    {
                        e.Row.Header = "Hard6";
                        break;
                    }
                case 2:
                    {
                        e.Row.Header = "Hard7";
                        break;
                    }
                case 3:
                    {
                        e.Row.Header = "Hard8";
                        break;
                    }
                case 4:
                    {
                        e.Row.Header = "Hard9";
                        break;
                    }
                case 5:
                    {
                        e.Row.Header = "Hard10";
                        break;
                    }
                case 6:
                    {
                        e.Row.Header = "Hard11";
                        break;
                    }
                case 7:
                    {
                        e.Row.Header = "Hard12";
                        break;
                    }
                case 8:
                    {
                        e.Row.Header = "Hard13";
                        break;
                    }
                case 9:
                    {
                        e.Row.Header = "Hard14";
                        break;
                    }
                case 10:
                    {
                        e.Row.Header = "Hard15";
                        break;
                    }
                case 11:
                    {
                        e.Row.Header = "Hard16";
                        break;
                    }
                case 12:
                    {
                        e.Row.Header = "Hard17";
                        break;
                    }
                case 13:
                    {
                        e.Row.Header = "Hard18";
                        break;
                    }
                case 14:
                    {
                        e.Row.Header = "Hard19";
                        break;
                    }
                case 15:
                    {
                        e.Row.Header = "Hard20";
                        break;
                    }
                case 16:
                    {
                        e.Row.Header = "Hard21";
                        break;
                    }
                case 17:
                    {
                        e.Row.Header = "Soft13";
                        break;
                    }
                case 18:
                    {
                        e.Row.Header = "Soft14";
                        break;
                    }
                case 19:
                    {
                        e.Row.Header = "Soft15";
                        break;
                    }
                case 20:
                    {
                        e.Row.Header = "Soft16";
                        break;
                    }
                case 21:
                    {
                        e.Row.Header = "Soft17";
                        break;
                    }
                case 22:
                    {
                        e.Row.Header = "Soft18";
                        break;
                    }
                case 23:
                    {
                        e.Row.Header = "Soft19";
                        break;
                    }
                case 24:
                    {
                        e.Row.Header = "Soft20";
                        break;
                    }
                case 25:
                    {
                        e.Row.Header = "Soft21";
                        break;
                    }
                case 26:
                    {
                        e.Row.Header = "Pair2";
                        break;
                    }
                case 27:
                    {
                        e.Row.Header = "Pair3";
                        break;
                    }
                case 28:
                    {
                        e.Row.Header = "Pair4";
                        break;
                    }
                case 29:
                    {
                        e.Row.Header = "Pair5";
                        break;
                    }
                case 30:
                    {
                        e.Row.Header = "Pair6";
                        break;
                    }
                case 31:
                    {
                        e.Row.Header = "Pair7";
                        break;
                    }
                case 32:
                    {
                        e.Row.Header = "Pair8";
                        break;
                    }
                case 33:
                    {
                        e.Row.Header = "Pair9";
                        break;
                    }
                case 34:
                    {
                        e.Row.Header = "Pair10";
                        break;
                    }
                case 35:
                    {
                        e.Row.Header = "PairA";
                        break;
                    }
                case 36:
                    {
                        e.Row.Header = "";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }

        // Loads only the HARD and SOFT row headers [26] to the strategy_datagrid
        private void dataGrid_LoadingHardSoftRowHeaders(object sender, DataGridRowEventArgs e)
        {
            var id = e.Row.GetIndex();
            switch (id)
            {
                case 0:
                    {
                        e.Row.Header = "Hard5";
                        break;
                    }
                case 1:
                    {
                        e.Row.Header = "Hard6";
                        break;
                    }
                case 2:
                    {
                        e.Row.Header = "Hard7";
                        break;
                    }
                case 3:
                    {
                        e.Row.Header = "Hard8";
                        break;
                    }
                case 4:
                    {
                        e.Row.Header = "Hard9";
                        break;
                    }
                case 5:
                    {
                        e.Row.Header = "Hard10";
                        break;
                    }
                case 6:
                    {
                        e.Row.Header = "Hard11";
                        break;
                    }
                case 7:
                    {
                        e.Row.Header = "Hard12";
                        break;
                    }
                case 8:
                    {
                        e.Row.Header = "Hard13";
                        break;
                    }
                case 9:
                    {
                        e.Row.Header = "Hard14";
                        break;
                    }
                case 10:
                    {
                        e.Row.Header = "Hard15";
                        break;
                    }
                case 11:
                    {
                        e.Row.Header = "Hard16";
                        break;
                    }
                case 12:
                    {
                        e.Row.Header = "Hard17";
                        break;
                    }
                case 13:
                    {
                        e.Row.Header = "Hard18";
                        break;
                    }
                case 14:
                    {
                        e.Row.Header = "Hard19";
                        break;
                    }
                case 15:
                    {
                        e.Row.Header = "Hard20";
                        break;
                    }
                case 16:
                    {
                        e.Row.Header = "Hard21";
                        break;
                    }
                case 17:
                    {
                        e.Row.Header = "Soft13";
                        break;
                    }
                case 18:
                    {
                        e.Row.Header = "Soft14";
                        break;
                    }
                case 19:
                    {
                        e.Row.Header = "Soft15";
                        break;
                    }
                case 20:
                    {
                        e.Row.Header = "Soft16";
                        break;
                    }
                case 21:
                    {
                        e.Row.Header = "Soft17";
                        break;
                    }
                case 22:
                    {
                        e.Row.Header = "Soft18";
                        break;
                    }
                case 23:
                    {
                        e.Row.Header = "Soft19";
                        break;
                    }
                case 24:
                    {
                        e.Row.Header = "Soft20";
                        break;
                    }
                case 25:
                    {
                        e.Row.Header = "Soft21";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }

        // Loads only the PAIR row headers [26] to the strategy_datagrid
        private void dataGrid_LoadingPairRowHeaders(object sender, DataGridRowEventArgs e)
        {
            var id = e.Row.GetIndex();
            switch (id)
            {
                case 0:
                    {
                        e.Row.Header = "Pair2   ";
                        break;
                    }
                case 1:
                    {
                        e.Row.Header = "Pair3 ";
                        break;
                    }
                case 2:
                    {
                        e.Row.Header = "Pair4   ";
                        break;
                    }
                case 3:
                    {
                        e.Row.Header = "Pair5   ";
                        break;
                    }
                case 4:
                    {
                        e.Row.Header = "Pair6   ";
                        break;
                    }
                case 5:
                    {
                        e.Row.Header = "Pair7   ";
                        break;
                    }
                case 6:
                    {
                        e.Row.Header = "Pair8   ";
                        break;
                    }
                case 7:
                    {
                        e.Row.Header = "Pair9   ";
                        break;
                    }
                case 8:
                    {
                        e.Row.Header = "Pair10   ";
                        break;
                    }
                case 9:
                    {
                        e.Row.Header = "PairA   ";
                        break;
                    }
                case 10:
                    {
                        e.Row.Header = "";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }

        // Creates the properties to get the and set the CAPS, NUM or SCROLL to ON.
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags,
        UIntPtr dwExtraInfo);
        const int KEYEVENTF_EXTENDEDKEY = 0x1;
        const int KEYEVENTF_KEYUP = 0x2;
        const int CAPSLOCK = 0x14;
        const int NUMLOCK = 0x90;
        const int SCROLLLOCK = 0x91;

        // Gets the state of CapsLock (on/off).
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        // A method to turn CapsLock on if the CapsLock is not currently on.
        private void ActivateCapsLock()
        {
            bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            if (!CapsLock)
            {
                keybd_event(CAPSLOCK, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
                keybd_event(CAPSLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
            }
        }

        // Converts the Datagrid context to a Data Table.
        public DataTable DataGridToTable(DataGrid dataGrid)
        {
            DataTable dataTable = new DataTable();
            dataTable = ((DataView)dataGrid.ItemsSource).ToTable();
            return dataTable;
        }

        // Merges two datagrids into one data table (Hard/Soft + Pair, datagrids)
        public DataTable DataGridMergeToTable(DataGrid datagrid_hard_soft, DataGrid datagrid_pair)
        {
            DataTable dataTable1 = new DataTable();
            dataTable1 = ((DataView)datagrid_hard_soft.ItemsSource).ToTable();
            DataTable dataTable2 = new DataTable();
            dataTable2 = ((DataView)datagrid_pair.ItemsSource).ToTable();
            dataTable1.Merge(dataTable2);
            return dataTable1;
        }
        #endregion

        #region - GETTERS AND SETTERS - INPUT DATA (Game Simulation) -
        // Gets the player strategy name from the textbox input
        public string GetPlayerStrategyNameInput()
        {
            return txtbPlayerStrategyName.Text;
        }

        // Gets the dealer strategy name from the textbox input
        public string GetDealerStrategyNameInput()
        {
            return txtbDealerStrategyName.Text;
        }

        // Gets the number of decks from the selected radio button input.
        public int GetNumberOfDecksInput()
        {
            if (rbtnDeck1.IsChecked.Value == true)
            {
                return 1;
            }
            if (rbtnDeck2.IsChecked.Value == true)
            {
                return 2;
            }
            if (rbtnDeck4.IsChecked.Value == true)
            {
                return 4;
            }
            if (rbtnDeck6.IsChecked.Value == true)
            {
                return 6;
            }
            if (rbtnDeck8.IsChecked.Value == true)
            {
                return 8;
            }
            return 0;
        }

        // Gets the double on option from the selected radio button input.
        public GameSimulationInput.EnumDoubleOn GetDoubleOnOptionInput()
        {
            if (rbtnDoubleOnAny2.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumDoubleOn.Any_2_Cards;
            }
            if (rbtnDoubleHardSoft9to11.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumDoubleOn.HardORSoft_9to11;
            }
            if (rbtnDoubleHard9to11.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumDoubleOn.Hard_9to11;
            }
            return GameSimulationInput.EnumDoubleOn.Any_2_Cards;
        }

        // Gets the double after split option from the check box input.
        public bool GetDoubleAfterSplitCBInput()
        {
            if (checkboxDoubleAfterSplit.IsChecked.Value == true)
            {
                return true;
            }
            else
                return false;
        }

        // Gets the surrender option from the selected surrender radio button input.
        public GameSimulationInput.EnumSurrender GetSurrenderOptionInput()
        {
            if (rbtnFullEarlySurrender.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumSurrender.Full_Early_Surrender;
            }
            if (rbtnLateSurrenderStandard.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumSurrender.Late_Standard_Surrender;
            }
            if (rbtnNoSurrender.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumSurrender.No_Surrender;
            }
            return GameSimulationInput.EnumSurrender.No_Surrender;
        }

        // Gets number of times the player can split from the Resplit radio buttons input.
        public int GetNumberOFResplitsFromOption()
        {
            if (rbtnNoResplits.IsChecked.Value == true)
            {
                return 1; // one is the default split amount and players can always split atleast once.
            }
            if (rbtnUpTo3Hands.IsChecked.Value == true)
            {
                return 2;
            }
            if (rbtnUpTo4Hands.IsChecked.Value == true)
            {
                return 3;
            }
            if (rbtnUpTo5Hands.IsChecked.Value == true)
            {
                return 4;
            }
            return 1;
        }

        // Gets the cannot split aces option from the check box input.
        public bool GetCannotSplitAcesCBInput()
        {
            if (checkboxCannotSplitAces.IsChecked.Value == true)
            {
                return true;
            }
            else
                return false;
        }

        // Gets the hit split aces option from the check box input.
        public bool GetHitSplitAcesCBInput()
        {
            if (checkboxHitSplitAces.IsChecked.Value == true)
            {
                return true;
            }
            else
                return false;
        }

        // Gets the cannot hit split 4s, 5s or 10s option from the check box input.
        public bool GetCannotHitSplit4s5s10sCBInput()
        {
            if (checkboxCannotSplit4s5sand10s.IsChecked.Value == true)
            {
                return true;
            }
            else
                return false;
        }

        // Gets the shuffle after each hand option from the check box input.
        public bool GetSuffleAfterEachHandCBInput()
        {
            if (checkboxShuffleAfterEachHand.IsChecked.Value == true)
            {
                return true;
            }
            else
                return false;
        }

        // Gets the Card Bonus option from the selected Card Bonus radio button input.
        public GameSimulationInput.EnumCardBonus GetCardBonusOptionInput()
        {
            if (rbtn5CardPlus21Pays2to1.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumCardBonus.Five_Above_Card_21_Pays2to1;
            }
            if (rbtn5Card21Pays2to1.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumCardBonus.Five_Card_21_Pays2to1;
            }
            if (rbtn5CardTrick.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumCardBonus.Five_Card_Trick_Bonus;
            }
            if (rbtnNoCardTrickBonus.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumCardBonus.No_Card_Trick_Bonus;
            }
            if (rbtn6CardTrick.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumCardBonus.Six_Card_Trick_Bonus;
            }
            if (rbtn7CardTrick.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumCardBonus.Seven_Card_Trick_Bonus;
            }
            return GameSimulationInput.EnumCardBonus.No_Card_Trick_Bonus;
        }

        // Gets the luck 777 option from the check box input.
        public bool GetLucky777CBInput()
        {
            if (checkboxLucky777Pays3to1.IsChecked.Value == true)
            {
                return true;
            }
            else
                return false;
        }

        // Gets the Blackjack Pays option from the selected Card Bonus radio button input.
        public GameSimulationInput.EnumBlackJackPays GetBlackjackPaysOptionInput()
        {
            if (rbtnBJPays1to1.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumBlackJackPays.Pays1to1;
            }
            if (rbtnBJPays2to1.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumBlackJackPays.Pays2to1;
            }
            if (rbtnBJPays3to2.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumBlackJackPays.Pays3to2;
            }
            if (rbtnBJPays6to5.IsChecked.Value == true)
            {
                return GameSimulationInput.EnumBlackJackPays.Pays6to5;
            }
            return GameSimulationInput.EnumBlackJackPays.Pays2to1;
        }

        // Gets the suited Blackjack pays 2 to 1 option from the check box input.
        public bool GetSuitedBJPays2to1CBInput()
        {
            if (checkboxShuffleAfterEachHand.IsChecked.Value == true)
            {
                return true;
            }
            else
                return false;
        }
        #endregion

        #region - Game Environment Input - Button Event Handlers - 

        // If the checkboxShuffleAfterEachHand_Checked is checked then the pecentage Shuffle slider will be set to 1.
        private void checkboxShuffleAfterEachHand_Checked(object sender, RoutedEventArgs e)
        {
            percentageSlider.Value = 1;
        }

        // If the shuffle percentage slider value is changed using the slider the checkboxShuffleAfterEachHand will be unchecked.
        private void percentageSlider_IsMouseCaptureWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            checkboxShuffleAfterEachHand.IsChecked = false;
        }

        // SAVES PLAYER STRATEGY: Converts the DataGrid (player strategy) values from DataTable to XML, and saves the XML in a strategy structure.
        private void btnSaveReport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savefd = new SaveFileDialog() { Filter = "Txt Documents|*.txt", ValidateNames = true };
            if (savefd.ShowDialog() == true)
            {
                List<XElement> elementList = new List<XElement>();

                if (File.Exists(savefd.FileName))
                {
                    File.WriteAllText(savefd.FileName, txtb_ReportView.Text);
                }
                else
                {
                    File.WriteAllText(savefd.FileName, txtb_ReportView.Text);

                }
                MessageBox.Show("Report saved.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Shows or hides the 'Key' inputs that can be typed into the Player Strategy Grid.
        private void btnShowPlayerKey_Click(object sender, RoutedEventArgs e)
        {
            if (lblPlayerStrategyKey.Visibility == Visibility.Visible)
                lblPlayerStrategyKey.Visibility = Visibility.Collapsed;
            else
                lblPlayerStrategyKey.Visibility = Visibility.Visible;
        }

        // Shows or hides the 'Key' inputs that can be typed into the Dealer Strategy Grid.
        private void btnShowDealerKey_Click(object sender, RoutedEventArgs e)
        {
            if (lblDealerStrategyKey.Visibility == Visibility.Visible)
                lblDealerStrategyKey.Visibility = Visibility.Collapsed;
            else
                lblDealerStrategyKey.Visibility = Visibility.Visible;
        }    
        #endregion



        #region - EVOLUTION SIMULATION -
        // Stores the current Generation count.
        private int _GENERATION_COUNT;

        // Gets all of the user inputs for the GA and creates a Evolution Simulation Input object.
        public void ReadAllEvolutionSimulationInputs()
        {
            // Sets the local variable Generation Counts for the GA simulation startup.
            _GENERATION_COUNT = GetGenerationCount();

            // Gets all of the GA input information, prepares all inputs to create a Evolution Simulation Input.
            int _INITIAL_BREEDING_POOL = GetInitialBreedingPool();
            int _MUTATION_CHANCE_PER_CELL = GetMutationChancePerCell();
            int _NEWBLOOD_CHANCE_PER_CHILD = GetNewBloodChancePerChild();
            EvolutionSimulationInput.EnumFitnessMeasurement _FITNESS_MEASUREMENT_OPTION = GetFitnessMeasurementOptionInput();
            int _CROSSOVER_ROW_CHANCE = GetCrossoverRowChance();
            int _CROSSOVER_COL_CHANCE = GetCrossoverColChance();
            int _STAND_HS_CHANCE = GetStandHSChance();
            int _HIT_HS_CHANCE = GetHitHSChance();
            int _DOUBLE_HS_CHANCE = GetDoubleHSChance();
            int _SURRENDER_HS_CHANCE = GetSurrenderHSChance();
            int _STAND_PAIR_CHANCE = GetStandPAIRChance();
            int _HIT_PAIR_CHANCE = GetHitPAIRChance();
            int _DOUBLE_PAIR_CHANCE = GetDoublePAIRChance();
            int _SPLIT_PAIR_CHANCE = GetSplitPAIRChance();
            int _SURRENDER_PAIR_CHANCE = GetSurrenderPAIRChance();
            bool _T_GENERATION_COUNT_ON = GetGenerationCountOn();
            int _T_GENERATION_COUNT = GetGenerationCount();
            bool _T_STAGNATION_POINT_ON = GetStagnationPointOn();
            int _T_STAGNATION_POINT = GetStagnationPoint();
            bool _T_TARGET_YIELD_ON = GetYieldTargetFitnessOn();
            decimal _T_TARGET_YIELD = GetYieldTargetFitness();
            bool _T_TARGET_PROFIT_ON = GetProfitTargetFitnessOn();
            int _T_TARGET_PROFIT = GetProfitTargetFitness();
            bool _T_TARGET_WINS_ON = GetWinsTargetFitnessOn();
            int _T_TARGET_WINS = GetWinsTargetFitness();

            // Creates a new Evolution Simulation Input and sets the _EVOLUTION_SIMULATION_INPUT variable, for later use in the Evolution Process.
            _EVOLUTION_SIMULATION_INPUT = new EvolutionSimulationInput(_INITIAL_BREEDING_POOL, _MUTATION_CHANCE_PER_CELL,
                _NEWBLOOD_CHANCE_PER_CHILD, _FITNESS_MEASUREMENT_OPTION, _CROSSOVER_ROW_CHANCE, _CROSSOVER_COL_CHANCE,
                _STAND_HS_CHANCE, _HIT_HS_CHANCE, _DOUBLE_HS_CHANCE, _SURRENDER_HS_CHANCE, _STAND_PAIR_CHANCE,
                _HIT_PAIR_CHANCE, _DOUBLE_PAIR_CHANCE, _SPLIT_PAIR_CHANCE, _SURRENDER_PAIR_CHANCE, _T_GENERATION_COUNT_ON, _T_GENERATION_COUNT, _T_STAGNATION_POINT_ON, _T_STAGNATION_POINT,
                _T_TARGET_YIELD_ON, _T_TARGET_YIELD, _T_TARGET_PROFIT_ON, _T_TARGET_PROFIT, _T_TARGET_WINS_ON, _T_TARGET_WINS);

            // Sets the Target Fitness for the Genetic Algorithm.
            switch (_FITNESS_MEASUREMENT_OPTION)
            {
                case EvolutionSimulationInput.EnumFitnessMeasurement.yield:
                    lblTargetFitnessText.Content = _T_TARGET_YIELD;
                    break;
                case EvolutionSimulationInput.EnumFitnessMeasurement.profit:
                    lblTargetFitnessText.Content = _T_TARGET_PROFIT;
                    break;
                case EvolutionSimulationInput.EnumFitnessMeasurement.wins:
                    lblTargetFitnessText.Content = _T_TARGET_WINS;
                    break;
            }
        }

        // Runs the main evolution process (in conjuction with the progress bar)
        private void RunEvolutionSimulation()
        {
            // Clears the Breeding Pool Logger View.
            ClearBreedingPoolListBox();

            // Initialises the Breeding Pool List.
            List<PlayerStrategy> _BreedingPoolList = new List<PlayerStrategy>();
            EvolutionSimulation.InitialiseEvolutionSimulation(_GAME_SIMULATION_INPUT, _EVOLUTION_SIMULATION_INPUT);
            int generationCount = 0;

            // The simulation looper, until the generation count is achieved (unless a termination condition).
            for (var loopCounter = 0.0; loopCounter <= 1.0; loopCounter = loopCounter + (double)decimal.Divide(1, _GENERATION_COUNT))
            {
                // Adds a counter to the currnet generation count.
                generationCount++;

                try
                {
                    // Updates the breeding pool list view.
                    _BreedingPoolList = EvolutionSimulation.RunGeneticAlgorithm();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                // If the Genetic Algorithm is not canceled then it will update the UI (Strategy List View)
                if (!IsGaCanceled)
                {
                    // Creates a list of the top 25 strategies in the breeding pool.
                    List<PlayerStrategy> eliteTop25List = new List<PlayerStrategy>();
                    eliteTop25List = _BreedingPoolList.Take(25).ToList();

                    // Adds a strategy to the strategy viewmodel collection to be bound to the listview.
                    Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { this.ViewModel.AddValue(eliteTop25List); }, null);

                    // Creates the EvolutionSimulation report for the best strategy (solution) in the breeding pool list.
                    EvolutionSimulationReport evolution_report = new EvolutionSimulationReport(eliteTop25List[0], eliteTop25List[0].GetGameSimulationReport(), _EVOLUTION_SIMULATION_INPUT, generationCount);

                    // Sets the Evolution sumulation output to the txtb_TheBestSolutionReport text box (UI).
                    Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { txtb_TheBestSolutionReport.Text = evolution_report.GetReportString().ToString(); }, null);

                    // Updates the current top fitness, and checks to see if the target fitness has been met.
                    switch (_EVOLUTION_SIMULATION_INPUT.GetFitnessMeasurementOption())
                    {
                        case EvolutionSimulationInput.EnumFitnessMeasurement.yield:
                            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { lblCurrentFitnessText.Content = eliteTop25List[0].GetFitnessYield(); }, null);
                            if (_EVOLUTION_SIMULATION_INPUT.GetTargetYield() < eliteTop25List[0].GetFitnessYield())
                            {
                                // If the target fitness is achieved then the evolution process will be cancelled.
                                CancelProcess();
                                MessageBox.Show("Target Fitness Achieved.");
                            }
                            break;
                        case EvolutionSimulationInput.EnumFitnessMeasurement.profit:

                            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { lblCurrentFitnessText.Content = eliteTop25List[0].GetFitnessProfit(); }, null);
                            if (_EVOLUTION_SIMULATION_INPUT.GetTargetProfit() < eliteTop25List[0].GetFitnessProfit())
                            {
                                // If the target fitness is achieved then the evolution process will be cancelled.
                                CancelProcess();
                                MessageBox.Show("Target Fitness Achieved.", "Simulation Message", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            break;
                        case EvolutionSimulationInput.EnumFitnessMeasurement.wins:

                            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { lblCurrentFitnessText.Content = eliteTop25List[0].GetFitnessWins(); }, null);
                            if (_EVOLUTION_SIMULATION_INPUT.GetTargetWins() < eliteTop25List[0].GetFitnessWins())
                            {
                                // If the target fitness is achieved then the evolution process will be cancelled.
                                CancelProcess();
                                MessageBox.Show("Target Fitness Achieved.", "Simulation Message", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            break;
                    }
                    // Check the stagnation status by checking the top strategy for its stagnation status.
                    if(eliteTop25List[0].GetIsStagnant() == true)
                    {
                        CancelProcess();
                        MessageBox.Show("Evolution hit stagmentation point.", "Simulation Message", MessageBoxButton.OK,MessageBoxImage.Exclamation);
                    }

                    // Updates the progress bar.
                    UpdateProgressBar(loopCounter);

                    // Sleep the thread for visual purposes.
                    //Thread.Sleep(10);
                }
                else
                {
                    break;
                }
            }
            // If the process is not cancelled the progress bar will update one more time (the last time) to make 100%. Then show a completed message.
            if (!IsGaCanceled)
            {
                UpdateProgressBar(1.0);
                MessageBox.Show("Evolution Complete.", "Simulation Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            IsGaCanceled = false;
            
            // Sets hides the progress bar and enables the begin button so the user can start a new simulation.
            UpdateProgressBar(1.0);
            EnableGaBeginButton();
            HideProgressBar();
        }

        // Saves the player strategy by converting the DataGrid (player strategy) values from DataTable to XML, and saves the XML in a strategy structure.
        private void btnExportEvolutionReport_Click(object sender, RoutedEventArgs e)
        {
            // Displays the save file dialog, filtering only txt. documents. 
            SaveFileDialog savefd = new SaveFileDialog() { Filter = "Txt Documents|*.txt", ValidateNames = true };

            // When the save button is clicked, save the best strategy text to a document.
            if (savefd.ShowDialog() == true)
            {
                List<XElement> elementList = new List<XElement>();

                if (File.Exists(savefd.FileName))
                {
                    File.WriteAllText(savefd.FileName, txtb_TheBestSolutionReport.Text);
                }
                else
                {
                    File.WriteAllText(savefd.FileName, txtb_TheBestSolutionReport.Text);
                }
                MessageBox.Show("Report saved.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #region - Termination Input Controls -

        // Check event: When the generation count check box is checked, the edit value buttons will show.
        private void checkboxGaGenerationCount_Checked(object sender, RoutedEventArgs e)
        {
            if (btnGaGenerationCountPlus == null || btnGaGenerationCountMinus == null)
                return;
            btnGaGenerationCountPlus.Visibility = Visibility.Visible;
            btnGaGenerationCountMinus.Visibility = Visibility.Visible;
            int startingCount = 2;
            txtbGaGenerationCount.Text = startingCount.ToString();

            // Ensures that the Stagnation Point is less than or equal to the Generation Target.
            int generationValue = Convert.ToInt32(txtbGaGenerationCount.Text);
            if (checkboxGaStagnationPoint.IsChecked == true)
            {
                int stagnationValue = Convert.ToInt32(txtbGaStagnationPoint.Text);
                if(stagnationValue > generationValue)
                {
                    txtbGaStagnationPoint.Text = generationValue.ToString();
                }
            }
        }

        // Check event: When the generation count check box is unchecked, the edit value buttons will be hidden.
        private void checkboxGaGenerationCount_Unchecked(object sender, RoutedEventArgs e)
        {
            btnGaGenerationCountPlus.Visibility = Visibility.Hidden;
            btnGaGenerationCountMinus.Visibility = Visibility.Hidden;
            txtbGaGenerationCount.Text = "∞";

        }

        // Click event: new generation up button is pressed.
        private void btnGaGenerationCountPlus_Click(object sender, RoutedEventArgs e)
        {
            // Gets the current value of the textbox.
            int current = Convert.ToInt32(txtbGaGenerationCount.Text);

            int MAX_GENERATIONS = 10000000;
            // If the current value is below the maximum minute input value.
            if (current < MAX_GENERATIONS)
            {
                // If the value is less than 5, it will increment by 1. Otherwise it will increment by 5 - until the value is 60, it will increment by 10.
                if (current < 5)
                {
                    current++;
                }
                else if (current >= 5 && current < 10)
                {
                    current += 5;
                }
                else if (current >= 10 && current < 100)
                {
                    current += 10;
                }
                else if (current >= 100 && current < 1000)
                {
                    current += 100;
                }
                else if (current >= 1000)
                {
                    current += 1000;
                }
                txtbGaGenerationCount.Text = current.ToString();
            }
        }

        // Click event: new generation down button is pressed.
        private void btnGaGenerationCountMinus_Click(object sender, RoutedEventArgs e)
        {
            // Gets the current value of the textbox.
            int generationValue = Convert.ToInt32(txtbGaGenerationCount.Text);

            int MAX_GENERATIONS = 1000000;
            // If the current value is below the maximum minute input value and not 1 or below.
            if (generationValue > 1 && generationValue < MAX_GENERATIONS)
            {
                // If the value is less than or equal 5, it will decrease by 1. Otherwise it will decrease by 5 - untill the value is greater than 60, it will decrease by 10.
                if (generationValue <= 5)
                {
                    generationValue--;
                }
                else if (generationValue > 5 && generationValue <= 10)
                {
                    generationValue -= 5;
                }
                else if (generationValue > 10 && generationValue <= 100)
                {
                    generationValue -= 10;
                }
                else if (generationValue > 100 && generationValue <= 1000)
                {
                    generationValue -= 100;
                }
                else if (generationValue > 1000)
                {
                    generationValue -= 1000;
                }
                txtbGaGenerationCount.Text = generationValue.ToString();
            }

            // Ensures that the Stagnation Point is less than or equal to the Generation Target.
            if (checkboxGaStagnationPoint.IsChecked == true)
            {
                int stagnationValue = Convert.ToInt32(txtbGaStagnationPoint.Text);
                if (stagnationValue > generationValue)
                {
                    txtbGaStagnationPoint.Text = generationValue.ToString();
                }
            }
        }

        // Check event: When the stagnation point check box is checked, the edit value buttons will show.
        private void checkboxGaStagnationPoint_Checked(object sender, RoutedEventArgs e)
        {
            btnGaStagnationPointPlus.Visibility = Visibility.Visible;
            btnGaStagnationPointMinus.Visibility = Visibility.Visible;
            int startingCount = 1;
            txtbGaStagnationPoint.Text = startingCount.ToString();
        }

        // Check event: When the stagnation point check box is unchecked, the edit value buttons will be hidden.
        private void checkboxGaStagnationPoint_Unchecked(object sender, RoutedEventArgs e)
        {
            btnGaStagnationPointPlus.Visibility = Visibility.Hidden;
            btnGaStagnationPointMinus.Visibility = Visibility.Hidden;
            txtbGaStagnationPoint.Text = "∞";

        }

        // Click event: stagnation point up button is pressed.
        private void btnGaStagnationPointPlus_Click(object sender, RoutedEventArgs e)
        {
            // Gets the current value of the textbox.
            int current = Convert.ToInt32(txtbGaStagnationPoint.Text);

            // Ensures that the Stagenation Point is less than or equal the total generations (if a target generation point has been set.
            if (checkboxGaGenerationCount.IsChecked == true)
            {
                // Gets the current value of the Generation Target textbox.
                int genValue = Convert.ToInt32(txtbGaGenerationCount.Text);
                if (current == genValue)
                    return; // Click returns (does nothing) if the Stagnation point will exceed the Generation Target.
            }
            int MAX_GENERATIONS = 1000000;
            // If the current value is below the maximum minute input value.
            if (current < MAX_GENERATIONS)
            {
                // If the value is less than 5, it will increment by 1. Otherwise it will increment by 5 - until the value is 60, it will increment by 10.
                if (current < 5)
                {
                    current++;
                }
                else if (current >= 5 && current < 10)
                {
                    current += 5;
                }
                else if (current >= 10 && current < 100)
                {
                    current += 10;
                }
                else if (current >= 100 && current < 1000)
                {
                    current += 100;
                }
                else if (current >= 1000)
                {
                    current += 1000;
                }
                txtbGaStagnationPoint.Text = current.ToString();
            }
        }

        // Click event: stagnation point down button is pressed.
        private void btnGaStagnationPointMinus_Click(object sender, RoutedEventArgs e)
        {
            // Gets the current value of the textbox.
            int current = Convert.ToInt32(txtbGaStagnationPoint.Text);

            int MAX_GENERATIONS = 10000000;
            // If the current value is below the maximum minute input value and not 1 or below.
            if (current != 1 && current < MAX_GENERATIONS)
            {
                // If the value is less than or equal 5, it will decrease by 1. Otherwise it will decrease by 5 - untill the value is greater than 60, it will decrease by 10.
                if (current <= 5)
                {
                    current--;
                }
                else if (current > 5 && current <= 10)
                {
                    current -= 5;
                }
                else if (current > 10 && current <= 100)
                {
                    current -= 10;
                }
                else if (current > 100 && current <= 1000)
                {
                    current -= 100;
                }
                else if (current > 1000)
                {
                    current -= 1000;
                }
                txtbGaStagnationPoint.Text = current.ToString();
            }
        }

        // Value changed event: Sends the value of the fitness slider to the corresponding fitness textbox.
        private void GaYieldFitness_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GaYieldFitness_Slider != null && txtbGaYieldFitnessTarget != null)
            {
                double sliderValue = Convert.ToDouble(GaYieldFitness_Slider.Value);
                double yieldValue = sliderValue * 0.001;
                txtbGaYieldFitnessTarget.Text = (yieldValue).ToString("0.000");
            }
        }

        // Value changed event: Sends the value of the fitness slider to the corresponding fitness textbox.
        private void GaProfitLossFitness_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtbGaProfitLossFitnessTarget.Text = GaProfitLossFitness_Slider.Value.ToString();
        }

        // Value changed event: Sends the value of the fitness slider to the corresponding fitness textbox.
        private void GaWinFitness_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtbGaWinsLossFitnessTarget.Text = GaWinsFitness_Slider.Value.ToString();
        }

        // Checked event: Links (checks) the fitness measurement (unit) to the fitness target (value).
        private void rbtnGaYieldFitness_Checked(object sender, RoutedEventArgs e)
        {
            // Sets the Yield Fitness radio button to the same as the radio button target for fitness.
            if (rbtnGaYieldFitnessTarget != null)
                rbtnGaYieldFitnessTarget.IsChecked = true;
        }

        // Checked event: Links (checks) the fitness measurement (unit) to the fitness target (value).
        private void rbtnGaProfitLoss_Checked(object sender, RoutedEventArgs e)
        {
            // Sets the Profit/Loss Fitness radio button to the same as the radio button target for fitness.
            if(rbtnGaProfitLossFitnessTarget !=null)
                rbtnGaProfitLossFitnessTarget.IsChecked = true;
        }

        // Checked event: Links (checks) the fitness measurement (unit) to the fitness target (value).
        private void rbtnGaWins_Checked(object sender, RoutedEventArgs e)
        {
            // Sets the Wins Fitness radio button to the same as the radio button target for fitness.
            if (rbtnGaWinsFitnessTarget != null)
                rbtnGaWinsFitnessTarget.IsChecked = true;
        }

        // Checked event: Sets the fitness values to the textbox and hide/shows the sliders.
        private void checkboxGaYieldFitnessTarget_Checked(object sender, RoutedEventArgs e)
        {
            // Sets the Yield Fitness textbox input to a yield number (0.00)
            if(txtbGaYieldFitnessTarget != null)
            txtbGaYieldFitnessTarget.Text = GaYieldFitness_Slider.Value.ToString();

            // Sets the Yield Fitness radio button to the same as the radio button target for fitness.
            rbtnFitnessYield.IsChecked = true;

            // Enable/Disable sliders
            if (GaYieldFitness_Slider != null && GaWinsFitness_Slider != null && GaProfitLossFitness_Slider != null)
            {
                GaYieldFitness_Slider.IsEnabled = true;
                GaWinsFitness_Slider.IsEnabled = false;
                GaProfitLossFitness_Slider.IsEnabled = false;
            }
        }

        // Unchecked event: Sets the fitness values to "∞" in the textbox.
        private void checkboxGaYieldFitnessTarget_Unchecked(object sender, RoutedEventArgs e)
        {
            // Sets the Yield Fitness textbox input to infinity (∞)
            if (txtbGaYieldFitnessTarget != null)
            {
                txtbGaYieldFitnessTarget.Text = "∞";
            }
        }

        // Checked event: Sets the fitness values to the textbox and hide/shows the sliders.
        private void checkboxGaProfitLossFitness_Checked(object sender, RoutedEventArgs e)
        {
            // Sets the Profit/Loss Fitness textbox input to a yield number (0)
            if (txtbGaProfitLossFitnessTarget != null)
                txtbGaProfitLossFitnessTarget.Text = GaProfitLossFitness_Slider.Value.ToString();

            // Sets the Profit/Loss Fitness radio button to the same as the radio button target for fitness.
            rbtnFitnessProfit.IsChecked = true;

            // Enable/Disable sliders
            if (GaYieldFitness_Slider != null && GaWinsFitness_Slider != null && GaProfitLossFitness_Slider != null)
            {
                GaYieldFitness_Slider.IsEnabled = false;
                GaWinsFitness_Slider.IsEnabled = false;
                GaProfitLossFitness_Slider.IsEnabled = true;
            }
        }

        // Unchecked event: Sets the fitness values to "∞" in the textbox.
        private void checkboxGaProfitLossFitness_Unchecked(object sender, RoutedEventArgs e)
        {
            // Sets the Profit/Loss Fitness textbox input to infinity (∞)
            if (txtbGaProfitLossFitnessTarget != null)
                txtbGaProfitLossFitnessTarget.Text = "∞";
        }

        // Checked event: Sets the fitness values to the textbox and hide/shows the sliders.
        private void checkboxGaWinsFitness_Checked(object sender, RoutedEventArgs e)
        {
            // Sets the Wins Fitness textbox input to a yield number (500)
            if (txtbGaWinsLossFitnessTarget != null)
                txtbGaWinsLossFitnessTarget.Text = GaWinsFitness_Slider.Value.ToString();


            // Sets the Wins Fitness radio button to the same as the radio button target for fitness.
            if (rbtnFitnessWins != null)
                rbtnFitnessWins.IsChecked = true;

            // Enable/Disable sliders
            if (GaYieldFitness_Slider != null && GaWinsFitness_Slider != null && GaProfitLossFitness_Slider != null)
            {
                GaYieldFitness_Slider.IsEnabled = false;
                GaWinsFitness_Slider.IsEnabled = true;
                GaProfitLossFitness_Slider.IsEnabled = false;
            }
        }

        // Unchecked event: Sets the fitness values to "∞" in the textbox.
        private void checkboxGaWinsFitness_Unchecked(object sender, RoutedEventArgs e)
        {
            // Sets the Wins Fitness textbox input to infinity (∞)
            if (txtbGaWinsLossFitnessTarget != null)
                txtbGaWinsLossFitnessTarget.Text = "∞";
        }
        #endregion

        #region - Progress Bar -
        // Determines whether the genetic algorith is running. 
        private bool IsGaCanceled = false;

        // Creates a new instance of the Strategy View Model for the list view.
        private StrategyViewModel _viewModel;
        public StrategyViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        // Creates a new StrategyViewModel and sets the properties to the view.
        private void PrepareStrategyViewModel()
        {
            // Creates a new StrategyViewModel which contains an ObservableCollection<Strategy>
            ViewModel = new StrategyViewModel();

            // Sets the breeding pool logger (to the list box).
            this.lstb_BreedingPoolLogger.DisplayMemberPath = "Value";
            this.lstb_BreedingPoolLogger.DataContext = ViewModel;
            this.lstb_BreedingPoolLogger.ItemsSource = ViewModel;
        }

        // Click event: Begin the Genetic algorithm, and chanve the (UI) view to the simulation screen.
        private void btnBeginGa_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = (tabControl.SelectedIndex + 1);
            txtb_TheBestSolutionReport.Text="Loading please wait...";
            BeginProcess();
        }

        // Click event: When the cancel button is clicked the Genetic Algorithm process will cancel (calling Cancel Process).
        private void btnCancelGa_Click(object sender, RoutedEventArgs e)
        {
            CancelProcess();
        }

        // Disables the begin button whilst the BJ Game simmulations are running
        private void DisableBeginButton()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { this.btnBeginGa.SetValue(Button.IsEnabledProperty, false); }, null);
        }

        // Displays the Progress bar and starts the EvolutionSimulation process. 
        public void BeginProcess()
        {
            // Gets all of the user inputs for the GA and creates a Evolution Simulation Input object.
            ReadAllEvolutionSimulationInputs();

            // Displays the progress bar.
            bdrProgressGa.Visibility = System.Windows.Visibility.Visible;
            DisableBeginButton();
            Action StartLoop;
            try
            {
                // Starts the Evolution Simulation process on a new thread.
                StartLoop = () => RunEvolutionSimulation();
                Thread GaThread;
                GaThread = new Thread(StartLoop.Invoke);
                GaThread.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        // Cancels the Evolution Simulation process.
        public void CancelProcess()
        {
            this.IsGaCanceled = true;
            Thread.Sleep(1500);
            EnableGaBeginButton();
        }

        // Enables the begin button on the UI.
        private void EnableGaBeginButton()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { this.btnBeginGa.SetValue(Button.IsEnabledProperty, true); }, null);
        }

        // Clears the Breeding Pool listbox.
        private void ClearBreedingPoolListBox()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { this.ViewModel.Clear(); }, null);
        }

        // Updates the progress bar's progress.
        private void UpdateProgressBar(double value)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { this.prgProgressGa.SetValue(ProgressBar.ValueProperty, value); }, null);
            int currentProgress = (int)(value * _GENERATION_COUNT);
            string prgString = currentProgress.ToString() + "/" + _GENERATION_COUNT;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { this.txtProgressGa.SetValue(TextBlock.TextProperty, prgString); }, null);
        }

        // Hides the progress bar.
        private void HideProgressBar()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { this.bdrProgressGa.SetValue(Border.VisibilityProperty, Visibility.Collapsed); }, null);
        }
        #endregion

        #region - GETTERS AND SETTERS - INPUT DATA (Evolution Simulation) -
        // Gets the initial breeding pool input value.
        public int GetInitialBreedingPool()
        {
            return Convert.ToInt32(txtbGaBreedingPool.Text);
        }

        // Gets the Mutation chance per cell value.
        public int GetMutationChancePerCell()
        {
            return Convert.ToInt32(txtbGaMutationChancePerCell.Text);
        }

        // Gets the New Blood change per child value.
        public int GetNewBloodChancePerChild()
        {
            return Convert.ToInt32(txtbGaNewBloodChancePerChild.Text);
        }

        // Gets the fitness measurement on option from the selected radio button input.
        public EvolutionSimulationInput.EnumFitnessMeasurement GetFitnessMeasurementOptionInput()
        {
            if (rbtnFitnessYield.IsChecked.Value == true)
            {
                return EvolutionSimulationInput.EnumFitnessMeasurement.yield;
            }
            if (rbtnFitnessProfit.IsChecked.Value == true)
            {
                return EvolutionSimulationInput.EnumFitnessMeasurement.profit;
            }
            if (rbtnFitnessWins.IsChecked.Value == true)
            {
                return EvolutionSimulationInput.EnumFitnessMeasurement.wins;
            }
            return EvolutionSimulationInput.EnumFitnessMeasurement.none;
        }

        // Gets the crossover row chance value.
        public int GetCrossoverRowChance()
        {
            return Convert.ToInt32(txtbGaRowCrossover.Text);
        }

        // Gets the crossover column chance value. 
        public int GetCrossoverColChance()
        {
            return Convert.ToInt32(txtbGaRowCrossover.Text);
        }

        // Gets the stand chance (Hard/Soft)
        public int GetStandHSChance()
        {
            return Convert.ToInt32(txtbGaStandAction_HS.Text);
        }

        // Gets the hit chance (Hard/Soft)
        public int GetHitHSChance()
        {
            return Convert.ToInt32(txtbGaHitAction_HS.Text);
        }

        // Gets the double chance (Hard/Soft)
        public int GetDoubleHSChance()
        {
            return Convert.ToInt32(txtbGaDoubleAction_HS.Text);
        }

        // Gets the surrender chance (Hard/Soft)
        public int GetSurrenderHSChance()
        {
            return Convert.ToInt32(txtbGaSurrenderAction_HS.Text);
        }

        // Gets the stand chance (Pair)
        public int GetStandPAIRChance()
        {
            return Convert.ToInt32(txtbGaStandAction_PAIR.Text);
        }

        // Gets the hit chance (Pair) 
        public int GetHitPAIRChance()
        {
            return Convert.ToInt32(txtbGaHitAction_PAIR.Text);
        }

        // Gets the double chance (Pair) 
        public int GetDoublePAIRChance()
        {
            return Convert.ToInt32(txtbGaDoubleAction_PAIR.Text);
        }

        // Gets the split chance (Pair) 
        public int GetSplitPAIRChance()
        {
            return Convert.ToInt32(txtbGaSplitAction_PAIR.Text);
        }

        // Gets the surrender chance (Pair) 
        public int GetSurrenderPAIRChance()
        {
            return Convert.ToInt32(txtbGaSurrenderAction_PAIR.Text);
        }

        /* TERMINATION CONDITIONS GETTERS */ 
        public bool GetGenerationCountOn()
        {
            if (checkboxGaGenerationCount.IsChecked == true)
            {
                return true;
            }
            else return false;
        }
        public int GetGenerationCount()
        {
            if (checkboxGaGenerationCount.IsChecked == true)
                return Convert.ToInt32(txtbGaGenerationCount.Text);
            else
                return 0;
        }
        public bool GetStagnationPointOn()
        {
            if (checkboxGaStagnationPoint.IsChecked == true)
            {
                return true;
            }
            else return false;
        }
        public int GetStagnationPoint()
        {
            if (checkboxGaStagnationPoint.IsChecked == true)
                return Convert.ToInt32(txtbGaStagnationPoint.Text);
            else
                return 0;
        }
        public bool GetYieldTargetFitnessOn()
        {
            if (rbtnFitnessYield.IsChecked == true)
            {
                return true;
            }
            else return false;
        }
        public decimal GetYieldTargetFitness()
        {
            if (rbtnGaYieldFitnessTarget.IsChecked == true)
            {
                return Convert.ToDecimal(txtbGaYieldFitnessTarget.Text);
            }
            else
                return 0;
        }
        public bool GetProfitTargetFitnessOn()
        {
            if (rbtnGaProfitLossFitnessTarget.IsChecked == true)
            {
                return true;
            }
            else return false;
        }
        public int GetProfitTargetFitness()
        {
            if (rbtnGaProfitLossFitnessTarget.IsChecked == true)
                return Convert.ToInt32(txtbGaProfitLossFitnessTarget.Text);
            else
                return 0;
        }
        public bool GetWinsTargetFitnessOn()
        {
            if (rbtnGaWinsFitnessTarget.IsChecked == true)
            {
                return true;
            }
            else return false;
        }
        public int GetWinsTargetFitness()
        {
            if (rbtnGaWinsFitnessTarget.IsChecked == true)
                return Convert.ToInt32(txtbGaWinsLossFitnessTarget.Text);
            else
                return 0;
        }
        #endregion

        #region - Genetic Algorithm Input - Button Event Handlers -

        // Value changed event: The row slider and column slider need to equal 100 (%). 
        private void GaRowSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GaColumnSlider == null || GaRowSlider == null)
                return;
            GaColumnSlider.Value = (100 - GaRowSlider.Value);
        }

        // Value changed event: The row slider and column slider need to equal 100 (%). 
        private void GaColumnSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GaColumnSlider == null || GaRowSlider == null)
                return;
            GaRowSlider.Value = (100 - GaColumnSlider.Value);
        }

        // Value changed event: The HARD/SOFT Soft slider values in total must equal to 100 (%).
        private void GaStandAction_HS_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GaStandAction_HS_Slider == null || GaHitAction_HS_Slider == null || GaDoubleAction_HS_Slider == null || GaSurrenderAction_HS_Slider == null)
                return;

            // If the total value is going to exceed 100 (%) then other sliders value will have to be reduced. 
            if ((GaDoubleAction_HS_Slider.Value + GaHitAction_HS_Slider.Value + GaSurrenderAction_HS_Slider.Value) > (100 - GaStandAction_HS_Slider.Value))
            {
                // Gets the average value of the remaining percentage weight.
                int averageValue = (int)(100 - GaStandAction_HS_Slider.Value) / 3;

                // These if statements find the largest of the two action (excluding surrender and the action being changed). Creating a smoother experience by subtracting from the largest.
                if (GaHitAction_HS_Slider.Value > GaDoubleAction_HS_Slider.Value)
                {
                    GaHitAction_HS_Slider.Value -= 1;
                    return;
                }
                else if (GaDoubleAction_HS_Slider.Value > GaHitAction_HS_Slider.Value)
                {
                    GaDoubleAction_HS_Slider.Value -= 1;
                    return;
                }

                // These if statements compare the average leftovers, and subtracts form the largest.
                if (GaDoubleAction_HS_Slider.Value > averageValue)
                {
                    GaDoubleAction_HS_Slider.Value -= 1;
                    return;
                }
                if (GaHitAction_HS_Slider.Value > averageValue)
                {
                    GaHitAction_HS_Slider.Value -= 1;
                    return;
                }
                if (GaSurrenderAction_HS_Slider.Value > averageValue)
                {
                    GaSurrenderAction_HS_Slider.Value -= 1;
                    return;
                }
            }
        }

        // Value changed event: The HARD/SOFT Soft slider values in total must equal to 100 (%).
        private void GaHitAction_HS_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GaStandAction_HS_Slider == null || GaHitAction_HS_Slider == null || GaDoubleAction_HS_Slider == null || GaSurrenderAction_HS_Slider == null)
                return;
            // If the total value is going to exceed 100 (%) then other sliders value will have to be reduced. 
            if ((GaStandAction_HS_Slider.Value + GaDoubleAction_HS_Slider.Value + GaSurrenderAction_HS_Slider.Value) > (100 - GaHitAction_HS_Slider.Value))
            {
                // Gets the average value of the remaining percentage weight.
                int averageValue = (int)(100 - GaHitAction_HS_Slider.Value) / 3;

                // These if statements find the largest of the two action (excluding surrender and the action being changed). Creating a smoother experience by subtracting from the largest.
                if (GaStandAction_HS_Slider.Value > GaDoubleAction_HS_Slider.Value)
                {
                    GaStandAction_HS_Slider.Value -= 1;
                    return;
                }
                else if (GaDoubleAction_HS_Slider.Value > GaStandAction_HS_Slider.Value)
                {
                    GaDoubleAction_HS_Slider.Value -= 1;
                    return;
                }

                // These if statements compare the average leftovers, and subtracts form the largest.
                if (GaStandAction_HS_Slider.Value > averageValue)
                {
                    GaStandAction_HS_Slider.Value -= 1;
                }
                else if (GaDoubleAction_HS_Slider.Value > averageValue)
                {
                    GaDoubleAction_HS_Slider.Value -= 1;
                }
                else if (GaSurrenderAction_HS_Slider.Value > averageValue)
                {
                    GaSurrenderAction_HS_Slider.Value -= 1;
                }
            }
        }

        // Value changed event: The HARD/SOFT Soft slider values in total must equal to 100 (%).
        private void GaDoubleAction_HS_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GaStandAction_HS_Slider == null || GaHitAction_HS_Slider == null || GaDoubleAction_HS_Slider == null || GaSurrenderAction_HS_Slider == null)
                return;

            // If the total value is going to exceed 100 (%) then other sliders value will have to be reduced.
            if ((GaStandAction_HS_Slider.Value + GaHitAction_HS_Slider.Value + GaSurrenderAction_HS_Slider.Value) > (100 - GaDoubleAction_HS_Slider.Value))
            {
                // Gets the average value of the remaining percentage weight.
                int averageValue = (int)(100 - GaDoubleAction_HS_Slider.Value) / 3;

                // These if statements find the largest of the two action (excluding surrender and the action being changed). Creating a smoother experience by subtracting from the largest.
                if (GaStandAction_HS_Slider.Value > GaHitAction_HS_Slider.Value)
                {
                    GaStandAction_HS_Slider.Value -= 1;
                    return;
                }
                else if (GaHitAction_HS_Slider.Value > GaStandAction_HS_Slider.Value)
                {
                    GaHitAction_HS_Slider.Value -= 1;
                    return;
                }

                // These if statements compare the average leftovers, and subtracts form the largest.
                if (GaStandAction_HS_Slider.Value > averageValue)
                {
                    GaStandAction_HS_Slider.Value -= 1;
                    return;
                }
                if (GaHitAction_HS_Slider.Value > averageValue)
                {
                    GaHitAction_HS_Slider.Value -= 1;
                    return;
                }
                if (GaSurrenderAction_HS_Slider.Value > averageValue)
                {
                    GaSurrenderAction_HS_Slider.Value -= 1;
                    return;
                }
            }
        }

        // Value changed event: The HARD/SOFT Soft slider values in total must equal to 100 (%).
        private void GaSurrenderAction_HS_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GaStandAction_HS_Slider == null || GaHitAction_HS_Slider == null || GaDoubleAction_HS_Slider == null || GaSurrenderAction_HS_Slider == null)
                return;

            // If the total value is going to exceed 100 (%) then other sliders value will have to be reduced.
            if ((GaStandAction_HS_Slider.Value + GaHitAction_HS_Slider.Value + GaDoubleAction_HS_Slider.Value) > (100 - GaSurrenderAction_HS_Slider.Value))
            {
                // Gets the average value of the remaining percentage weight.
                int averageValue = (int)(100 - GaSurrenderAction_HS_Slider.Value) / 3;

                // These if statements compare the average leftovers, and subtracts form the largest.
                if (GaStandAction_HS_Slider.Value > averageValue)
                {
                    GaStandAction_HS_Slider.Value -= 1;
                }
                else if (GaHitAction_HS_Slider.Value > averageValue)
                {
                    GaHitAction_HS_Slider.Value -= 1;
                }
                else if (GaDoubleAction_HS_Slider.Value > averageValue)
                {
                    GaDoubleAction_HS_Slider.Value -= 1;
                }
            }
        }

        // Value changed event: The PAIR slider values in total must equal to 100 (%).
        private void GaStandAction_PAIR_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GaStandAction_PAIR_Slider == null || GaHitAction_PAIR_Slider == null || GaDoubleAction_PAIR_Slider == null ||
                GaSurrenderAction_PAIR_Slider == null || GaSplitAction_PAIR_Slider == null)
                return;
            // If the total value is going to exceed 100 (%) then other sliders value will have to be reduced. 
            if ((GaDoubleAction_PAIR_Slider.Value + GaHitAction_PAIR_Slider.Value + GaSplitAction_PAIR_Slider.Value + GaSurrenderAction_PAIR_Slider.Value) > (100 - GaStandAction_PAIR_Slider.Value))
            {
                // Gets the average value of the remaining percentage weight.
                int averageValue = (int)(100 - GaStandAction_PAIR_Slider.Value) / 3;

                // These if statements find the largest of the three action (excluding surrender and the action being changed). Creating a smoother experience by subtracting from the largest.
                if (GaHitAction_PAIR_Slider.Value > GaDoubleAction_PAIR_Slider.Value && GaHitAction_PAIR_Slider.Value > GaSplitAction_PAIR_Slider.Value)
                {
                    GaHitAction_PAIR_Slider.Value -= 1;
                    return;
                }
                else if (GaDoubleAction_PAIR_Slider.Value > GaHitAction_PAIR_Slider.Value && GaDoubleAction_PAIR_Slider.Value > GaSplitAction_PAIR_Slider.Value)
                {
                    GaDoubleAction_PAIR_Slider.Value -= 1;
                    return;
                }
                else if (GaSplitAction_PAIR_Slider.Value > GaHitAction_PAIR_Slider.Value && GaSplitAction_PAIR_Slider.Value > GaDoubleAction_HS_Slider.Value)
                {
                    GaSplitAction_PAIR_Slider.Value -= 1;
                    return;
                }

                // If all of the values are the same (except the surrender value and the action value that is changing). Decrease one of the same values.
                if (GaDoubleAction_PAIR_Slider.Value == GaHitAction_PAIR_Slider.Value)
                {
                    GaDoubleAction_PAIR_Slider.Value -= 1;
                    return;
                }

                // These if statements compare the average leftovers, and subtracts form the largest.
                if (GaDoubleAction_PAIR_Slider.Value > averageValue)
                {
                    GaDoubleAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaHitAction_PAIR_Slider.Value > averageValue)
                {
                    GaHitAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaSplitAction_PAIR_Slider.Value > averageValue)
                {
                    GaSplitAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaSurrenderAction_PAIR_Slider.Value > averageValue)
                {
                    GaSurrenderAction_PAIR_Slider.Value -= 1;
                    return;
                }
                // When all values are the same
                GaSplitAction_PAIR_Slider.Value -= 1;
            }
        }

        // Value changed event: The PAIR slider values in total must equal to 100 (%).
        private void GaHitAction_PAIR_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GaStandAction_PAIR_Slider == null || GaHitAction_PAIR_Slider == null || GaDoubleAction_PAIR_Slider == null ||
                GaSurrenderAction_PAIR_Slider == null || GaSplitAction_PAIR_Slider == null)
                return;
            // If the total value is going to exceed 100 (%) then other sliders value will have to be reduced. 
            if ((GaDoubleAction_PAIR_Slider.Value + GaStandAction_PAIR_Slider.Value + GaSplitAction_PAIR_Slider.Value + GaSurrenderAction_PAIR_Slider.Value) > (100 - GaHitAction_PAIR_Slider.Value))
            {
                // Gets the average value of the remaining percentage weight.
                int averageValue = (int)(100 - GaHitAction_PAIR_Slider.Value) / 4;

                // These if statements find the largest of the three action (excluding surrender and the action being changed). Creating a smoother experience by subtracting from the largest.
                if (GaStandAction_PAIR_Slider.Value > GaDoubleAction_PAIR_Slider.Value && GaStandAction_PAIR_Slider.Value > GaSplitAction_PAIR_Slider.Value)
                {
                    GaStandAction_PAIR_Slider.Value -= 1;
                    return;
                }
                else if (GaDoubleAction_PAIR_Slider.Value > GaStandAction_PAIR_Slider.Value && GaDoubleAction_PAIR_Slider.Value > GaSplitAction_PAIR_Slider.Value)
                {
                    GaDoubleAction_PAIR_Slider.Value -= 1;
                    return;
                }
                else if (GaSplitAction_PAIR_Slider.Value > GaStandAction_PAIR_Slider.Value && GaSplitAction_PAIR_Slider.Value > GaDoubleAction_HS_Slider.Value)
                {
                    GaSplitAction_PAIR_Slider.Value -= 1;
                    return;
                }

                // If all of the values are the same (except the surrender value and the action value that is changing). Decrease one of the same values.
                if (GaDoubleAction_PAIR_Slider.Value == GaStandAction_PAIR_Slider.Value)
                {
                    GaDoubleAction_PAIR_Slider.Value -= 1;
                    return;
                }

                // These if statements compare the average leftovers, and subtracts form the largest.
                if (GaDoubleAction_PAIR_Slider.Value > averageValue)
                {
                    GaDoubleAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaStandAction_PAIR_Slider.Value > averageValue)
                {
                    GaStandAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaSplitAction_PAIR_Slider.Value > averageValue)
                {
                    GaSplitAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaSurrenderAction_PAIR_Slider.Value > averageValue)
                {
                    GaSurrenderAction_PAIR_Slider.Value -= 1;
                    return;
                }
                // When all values are the same
                GaSplitAction_PAIR_Slider.Value -= 1;
            }
        }

        // Value changed event: The PAIR slider values in total must equal to 100 (%).
        private void GaDoubleAction_PAIR_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GaStandAction_PAIR_Slider == null || GaHitAction_PAIR_Slider == null || GaDoubleAction_PAIR_Slider == null ||
                GaSurrenderAction_PAIR_Slider == null || GaSplitAction_PAIR_Slider == null)
                return;
            // If the total value is going to exceed 100 (%) then other sliders value will have to be reduced. 
            if ((GaHitAction_PAIR_Slider.Value + GaStandAction_PAIR_Slider.Value + GaSplitAction_PAIR_Slider.Value + GaSurrenderAction_PAIR_Slider.Value) > (100 - GaDoubleAction_PAIR_Slider.Value))
            {
                // Gets the average value of the remaining percentage weight.
                int averageValue = (int)(100 - GaDoubleAction_PAIR_Slider.Value) / 4;

                // These if statements find the largest of the three action (excluding surrender and the action being changed). Creating a smoother experience by subtracting from the largest.
                if (GaStandAction_PAIR_Slider.Value > GaHitAction_PAIR_Slider.Value && GaStandAction_PAIR_Slider.Value > GaSplitAction_PAIR_Slider.Value)
                {
                    GaStandAction_PAIR_Slider.Value -= 1;
                    return;
                }
                else if (GaHitAction_PAIR_Slider.Value > GaStandAction_PAIR_Slider.Value && GaHitAction_PAIR_Slider.Value > GaSplitAction_PAIR_Slider.Value)
                {
                    GaHitAction_PAIR_Slider.Value -= 1;
                    return;
                }
                else if (GaSplitAction_PAIR_Slider.Value > GaStandAction_PAIR_Slider.Value && GaSplitAction_PAIR_Slider.Value > GaHitAction_PAIR_Slider.Value)
                {
                    GaSplitAction_PAIR_Slider.Value -= 1;
                    return;
                }

                // If all of the values are the same (except the surrender value and the action value that is changing). Decrease one of the same values.
                if (GaHitAction_PAIR_Slider.Value == GaStandAction_PAIR_Slider.Value)
                {
                    GaHitAction_PAIR_Slider.Value -= 1;
                    return;
                }

                // These if statements compare the average leftovers, and subtracts form the largest.
                if (GaHitAction_PAIR_Slider.Value > averageValue)
                {
                    GaHitAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaStandAction_PAIR_Slider.Value > averageValue)
                {
                    GaStandAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaSplitAction_PAIR_Slider.Value > averageValue)
                {
                    GaSplitAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaSurrenderAction_PAIR_Slider.Value > averageValue)
                {
                    GaSurrenderAction_PAIR_Slider.Value -= 1;
                    return;
                }
                // When all values are the same
                GaSplitAction_PAIR_Slider.Value -= 1;
            }
        }

        // Value changed event: The PAIR slider values in total must equal to 100 (%).
        private void GaSplitAction_PAIR_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GaStandAction_PAIR_Slider == null || GaHitAction_PAIR_Slider == null || GaDoubleAction_PAIR_Slider == null ||
                GaSurrenderAction_PAIR_Slider == null || GaSplitAction_PAIR_Slider == null)
                return;
            // If the total value is going to exceed 100 (%) then other sliders value will have to be reduced. 
            if ((GaHitAction_PAIR_Slider.Value + GaStandAction_PAIR_Slider.Value + GaDoubleAction_PAIR_Slider.Value + GaSurrenderAction_PAIR_Slider.Value) > (100 - GaSplitAction_PAIR_Slider.Value))
            {
                // Gets the average value of the remaining percentage weight.
                int averageValue = (int)(100 - GaSplitAction_PAIR_Slider.Value) / 4;

                // These if statements find the largest of the three action (excluding surrender and the action being changed). Creating a smoother experience by subtracting from the largest.
                if (GaStandAction_PAIR_Slider.Value > GaHitAction_PAIR_Slider.Value && GaStandAction_PAIR_Slider.Value > GaDoubleAction_PAIR_Slider.Value)
                {
                    GaStandAction_PAIR_Slider.Value -= 1;
                    return;
                }
                else if (GaHitAction_PAIR_Slider.Value > GaStandAction_PAIR_Slider.Value && GaHitAction_PAIR_Slider.Value > GaDoubleAction_PAIR_Slider.Value)
                {
                    GaHitAction_PAIR_Slider.Value -= 1;
                    return;
                }
                else if (GaDoubleAction_PAIR_Slider.Value > GaStandAction_PAIR_Slider.Value && GaDoubleAction_PAIR_Slider.Value > GaHitAction_PAIR_Slider.Value)
                {
                    GaDoubleAction_PAIR_Slider.Value -= 1;
                    return;
                }

                // If all of the values are the same (except the surrender value and the action value that is changing). Decrease one of the same values.
                if (GaHitAction_PAIR_Slider.Value == GaStandAction_PAIR_Slider.Value)
                {
                    GaHitAction_PAIR_Slider.Value -= 1;
                    return;
                }

                // These if statements compare the average leftovers, and subtracts form the largest.
                if (GaHitAction_PAIR_Slider.Value > averageValue)
                {
                    GaHitAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaStandAction_PAIR_Slider.Value > averageValue)
                {
                    GaStandAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaDoubleAction_PAIR_Slider.Value > averageValue)
                {
                    GaDoubleAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaSurrenderAction_PAIR_Slider.Value > averageValue)
                {
                    GaSurrenderAction_PAIR_Slider.Value -= 1;
                    return;
                }
                // When all values are the same
                GaDoubleAction_PAIR_Slider.Value -= 1;
            }
        }

        // Value changed event: The PAIR slider values in total must equal to 100 (%).
        private void GaSurrenderAction_PAIR_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (GaStandAction_PAIR_Slider == null || GaHitAction_PAIR_Slider == null || GaDoubleAction_PAIR_Slider == null ||
                GaSurrenderAction_PAIR_Slider == null || GaSplitAction_PAIR_Slider == null)
                return;
            // If the total value is going to exceed 100 (%) then other sliders value will have to be reduced. 
            if ((GaHitAction_PAIR_Slider.Value + GaStandAction_PAIR_Slider.Value + GaDoubleAction_PAIR_Slider.Value + GaSplitAction_PAIR_Slider.Value) > (100 - GaSurrenderAction_PAIR_Slider.Value))
            {
                // Gets the average value of the remaining percentage weight.
                int averageValue = (int)(100 - GaSurrenderAction_PAIR_Slider.Value) / 4;

                // These if statements find the largest of the three action (excluding surrender and the action being changed). Creating a smoother experience by subtracting from the largest.
                if (GaStandAction_PAIR_Slider.Value > GaHitAction_PAIR_Slider.Value && GaStandAction_PAIR_Slider.Value > GaSplitAction_PAIR_Slider.Value)
                {
                    GaStandAction_PAIR_Slider.Value -= 1;
                    return;
                }
                else if (GaHitAction_PAIR_Slider.Value > GaStandAction_PAIR_Slider.Value && GaHitAction_PAIR_Slider.Value > GaSplitAction_PAIR_Slider.Value)
                {
                    GaHitAction_PAIR_Slider.Value -= 1;
                    return;
                }
                else if (GaSplitAction_PAIR_Slider.Value > GaStandAction_PAIR_Slider.Value && GaSplitAction_PAIR_Slider.Value > GaHitAction_PAIR_Slider.Value)
                {
                    GaSplitAction_PAIR_Slider.Value -= 1;
                    return;
                }

                // These if statements compare the average leftovers, and subtracts form the largest.
                if (GaHitAction_PAIR_Slider.Value > averageValue)
                {
                    GaHitAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaStandAction_PAIR_Slider.Value > averageValue)
                {
                    GaStandAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaDoubleAction_PAIR_Slider.Value > averageValue)
                {
                    GaDoubleAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaSurrenderAction_PAIR_Slider.Value > averageValue)
                {
                    GaSurrenderAction_PAIR_Slider.Value -= 1;
                    return;
                }

                // If all of the values are the same (except the surrender value and the action value that is changing). Decrease one of the same values.
                if (GaHitAction_PAIR_Slider.Value == GaStandAction_PAIR_Slider.Value)
                {
                    GaHitAction_PAIR_Slider.Value -= 1;
                    return;
                }
                if (GaSplitAction_PAIR_Slider.Value == GaStandAction_PAIR_Slider.Value)
                {
                    GaSplitAction_PAIR_Slider.Value -= 1;
                    return;
                }

                // When all values are the same
                GaDoubleAction_PAIR_Slider.Value -= 1;
            }
        }

        // Click event: Shows the Edit button or Default button to change the breeding pool input value.
        private void btnGaBreedingPool_Click(object sender, RoutedEventArgs e)
        {
            if (btnGaBreedingPool.Content.ToString() == "Default")
            {
                btnGaBreedingPool.Content = "Edit";
                txtbGaBreedingPool.Text = "100";
                txtbGaBreedingPool.Foreground = Brushes.Gray;
                lblGaBreedingPool.Foreground = Brushes.Gray;
                btnGaBreedingPoolPlus.Visibility = Visibility.Hidden;
                btnGaBreedingPoolMinus.Visibility = Visibility.Hidden;
            }
            else if (btnGaBreedingPool.Content.ToString() == "Edit")
            {
                btnGaBreedingPool.Content = "Default";
                txtbGaBreedingPool.Foreground = Brushes.Black;
                lblGaBreedingPool.Foreground = Brushes.Black;
                btnGaBreedingPoolPlus.Visibility = Visibility.Visible;
                btnGaBreedingPoolMinus.Visibility = Visibility.Visible;
            }
        }

        // Click event: Shows the Edit button or Default button to change the mutation change per cell input value.
        private void btnGaMutationChancePerCell_Click(object sender, RoutedEventArgs e)
        {
            if (btnGaMutationChancePerCell.Content.ToString() == "Default")
            {
                btnGaMutationChancePerCell.Content = "Edit";
                txtbGaMutationChancePerCell.Text = "5";
                txtbGaMutationChancePerCell.Foreground = Brushes.Gray;
                lblGaMutationChancePerCell.Foreground = Brushes.Gray;
                btnGaMutationChancePerCellPlus.Visibility = Visibility.Hidden;
                btnGaMutationChancePerCellMinus.Visibility = Visibility.Hidden;
            }
            else if (btnGaMutationChancePerCell.Content.ToString() == "Edit")
            {
                btnGaMutationChancePerCell.Content = "Default";
                txtbGaMutationChancePerCell.Foreground = Brushes.Black;
                lblGaMutationChancePerCell.Foreground = Brushes.Black;
                btnGaMutationChancePerCellPlus.Visibility = Visibility.Visible;
                btnGaMutationChancePerCellMinus.Visibility = Visibility.Visible;
            }
        }

        // Click event: Shows the Edit button or Default button to change the new blood change per child input value.
        private void btnGaNewBloodChancePerChild_Click(object sender, RoutedEventArgs e)
        {
            if (btnGaNewBloodChancePerChild.Content.ToString() == "Default")
            {
                btnGaNewBloodChancePerChild.Content = "Edit";
                txtbGaNewBloodChancePerChild.Text = "3";
                txtbGaNewBloodChancePerChild.Foreground = Brushes.Gray;
                lblGaNewBloodChancePerChild.Foreground = Brushes.Gray;
                btnGaNewBloodChancePerChildPlus.Visibility = Visibility.Hidden;
                btnGaNewBloodChancePerChildMinus.Visibility = Visibility.Hidden;
            }
            else if (btnGaNewBloodChancePerChild.Content.ToString() == "Edit")
            {
                btnGaNewBloodChancePerChild.Content = "Default";
                txtbGaNewBloodChancePerChild.Foreground = Brushes.Black;
                lblGaNewBloodChancePerChild.Foreground = Brushes.Black;
                btnGaNewBloodChancePerChildPlus.Visibility = Visibility.Visible;
                btnGaNewBloodChancePerChildMinus.Visibility = Visibility.Visible;
            }
        }

        // Click event: Breeding pool up button is pressed.
        private void btnGaBreedingPoolPlus_Click(object sender, RoutedEventArgs e)
        {
            // Gets the current value of the textbox.
            int current = Convert.ToInt32(txtbGaBreedingPool.Text);

            int MAX_POOL = 500;
            // If the current value is below the maximum pool input value.
            if (current < MAX_POOL)
            {
                // If the value is less than 5, it will increment by 1. Otherwise it will increment by 5 - until the value is 60, it will increment by 10, then 50.
                if (current < 5)
                {
                    current++;
                }
                else if (current >= 5 && current < 60)
                {
                    current += 5;
                }
                else if (current >= 60 && current < 100)
                {
                    current += 10;
                }
                else if (current >= 100)
                {
                    current += 50;
                }
                txtbGaBreedingPool.Text = current.ToString();
            }
        }

        // Click event: Breeding pool down button is pressed.
        private void btnGaBreedingPoolMinus_Click(object sender, RoutedEventArgs e)
        {
            // Gets the current value of the textbox.
            int current = Convert.ToInt32(txtbGaBreedingPool.Text);

            int MAX_POOL = 500;
            // If the current value is below the maximum pool input value and not 1 or below.
            if (current != 1 && current < MAX_POOL)
            {
                // If the value is less than or equal 5, it will decrease by 1. Otherwise it will decrease by 5 - untill the value is greater than 60, it will decrease by 10.
                if (current <= 5)
                {
                    current--;
                }
                else if (current > 5 && current <= 60)
                {
                    current -= 5;
                }
                else if (current > 60 && current <= 100)
                {
                    current -= 10;
                }
                else if (current > 100)
                {
                    current -= 50;
                }
                txtbGaBreedingPool.Text = current.ToString();
            }
        }

        // Click event: mutation change up button is pressed.
        private void btnGaMutationChancePerCellPlus_Click(object sender, RoutedEventArgs e)
        {
            // Gets the current value of the textbox.
            int current = Convert.ToInt32(txtbGaMutationChancePerCell.Text);

            int MAX_PERCENTAGE = 100;
            // If the current value is below the maximum (%) input value.
            if (current < MAX_PERCENTAGE)
            {
                current = current + 1;
                txtbGaMutationChancePerCell.Text = current.ToString();
            }
        }

        // Click event: mutation change down button is pressed.
        private void btnGaMutationChancePerCellMinus_Click(object sender, RoutedEventArgs e)
        {
            // Gets the current value of the textbox.
            int current = Convert.ToInt32(txtbGaMutationChancePerCell.Text);

            int MAX_PERCENTAGE = 100;
            // If the current value is below the maximum (%) input value and not 1 or below.
            if (current != 1 && current < MAX_PERCENTAGE)
            {
                current = current - 1;
                txtbGaMutationChancePerCell.Text = current.ToString();
            }
        }


        // Click event: new blood change up button is pressed.
        private void btnGaNewBloodChancePerChildPlus_Click(object sender, RoutedEventArgs e)
        {
            // Gets the current value of the textbox.
            int current = Convert.ToInt32(txtbGaNewBloodChancePerChild.Text);

            int MAX_PERCENTAGE = 100;
            // If the current value is below the maximum (%) input value.
            if (current < MAX_PERCENTAGE)
            {
                current = current + 1;
                txtbGaNewBloodChancePerChild.Text = current.ToString();
            }
        }

        // Click event: new blood change down button is pressed.
        private void btnGaNewBloodChancePerChildMinus_Click(object sender, RoutedEventArgs e)
        {
            // Gets the current value of the textbox.
            int current = Convert.ToInt32(txtbGaNewBloodChancePerChild.Text);

            int MAX_PERCENTAGE = 100;
            // If the current value is below the maximum (%) input value and not 1 or below.
            if (current != 1 && current < MAX_PERCENTAGE)
            {
                current = current - 1;
                txtbGaNewBloodChancePerChild.Text = current.ToString();
            }
        }
        #endregion
    }
}
