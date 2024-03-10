using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameController : MonoBehaviour
{

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI[] buttonList;
    private string playerSide;
    private int moveCount;

    private string aiSide = "O";
    private string humanSide = "X";

    private void Awake() {
        playerSide = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);
        SetControllerOnButtons();
    }

    void SetControllerOnButtons() {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<GridSpace>().SetController(this);
        }
    }

    public string GetPlayerSide() {
        // COME BACK HERE LATER
        return playerSide;
    }

    public void EndTurn() {
        moveCount++;

        // Top Row
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide) {
            GameOver(playerSide);
        }
        // Middle Row
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide) {
            GameOver(playerSide);
        }
        // Bottom Row
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide) {
            GameOver(playerSide);
        }
        // Left Column
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide) {
            GameOver(playerSide);
        }
        // Middle Column
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide) {
            GameOver(playerSide);
        }
        // Right Column
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide) {
            GameOver(playerSide);
        }
        // Falling Diagonal
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide) {
            GameOver(playerSide);
        }
        // Rising Diagonal
        else if (buttonList[6].text == playerSide && buttonList[4].text == playerSide && buttonList[2].text == playerSide) {
            GameOver(playerSide);
        }
        else if (moveCount >= 9) {
            GameOver("Draw");
        }
        else {
            ChangeSides();
            if (playerSide == "O") {
                ComputerTurn();
            }
        }
    }

    void GameOver(string winningPlayer) {
        SetBoardInteractable(false);
        if (winningPlayer == "Draw") {
            SetGameOverText("It's a Draw!");
        }
        else {
            SetGameOverText(playerSide + " Wins!");
        }
    }

    void ChangeSides() {
        playerSide = (playerSide == "X") ? "O" : "X";
    }

    void SetGameOverText(string myText) {
        gameOverText.text = myText;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SetBoardInteractable(bool toggle) {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }
    void ComputerTurn() {
        int bestScore = int.MinValue;
        int bestMove = -1;
        for (int i = 0; i < buttonList.Length; i++)
        {
            if (buttonList[i].text == "")
            {
                buttonList[i].text = aiSide; // Make the move
                int score = Minimax(false);
                buttonList[i].text = ""; // Undo the move
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = i;
                }
            }
        }

        if (bestMove != -1)
        {
            buttonList[bestMove].text = aiSide;
            buttonList[bestMove].GetComponentInParent<Button>().interactable = false;
            EndTurn();
        }
    }

    int Minimax(bool isMaximizing)
    {
        string result = CheckWinner();
        if (result != null)
        {
            return Score(result);
        }

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int i = 0; i < buttonList.Length; i++)
            {
                if (buttonList[i].text == "")
                {
                    buttonList[i].text = aiSide;
                    int score = Minimax(false);
                    buttonList[i].text = "";
                    bestScore = Mathf.Max(score, bestScore);
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int i = 0; i < buttonList.Length; i++)
            {
                if (buttonList[i].text == "")
                {
                    buttonList[i].text = humanSide;
                    int score = Minimax(true);
                    buttonList[i].text = "";
                    bestScore = Mathf.Min(score, bestScore);
                }
            }
            return bestScore;
        }
    }

    int Score(string winner)
    {
        if (winner == aiSide)
            return 10;
        else if (winner == humanSide)
            return -10;
        else
            return 0;
    }

    string CheckWinner()
    {

        // O Winning
        // Top Row
        if (buttonList[0].text == "O" && buttonList[1].text == "O" && buttonList[2].text == "O") {
            return "O";
        }
        // Middle Row
        else if (buttonList[3].text == "O" && buttonList[4].text == "O" && buttonList[5].text == "O") {
            return "O";
        }
        // Bottom Row
        else if (buttonList[6].text == "O" && buttonList[7].text == "O" && buttonList[8].text == "O") {
            return "O";
        }
        // Left Column
        else if (buttonList[0].text == "O" && buttonList[3].text == "O" && buttonList[6].text == "O") {
            return "O";
        }
        // Middle Column
        else if (buttonList[1].text == "O" && buttonList[4].text == "O" && buttonList[7].text == "O") {
            return "O";
        }
        // Right Column
        else if (buttonList[2].text == "O" && buttonList[5].text == "O" && buttonList[8].text == "O") {
            return "O";
        }
        // Falling Diagonal
        else if (buttonList[0].text == "O" && buttonList[4].text == "O" && buttonList[8].text == "O") {
            return "O";
        }
        // Rising Diagonal
        else if (buttonList[6].text == "O" && buttonList[4].text == "O" && buttonList[2].text == "O") {
            return "O";
        }

        // X Winning
        // Top Row
        else if (buttonList[0].text == "X" && buttonList[1].text == "X" && buttonList[2].text == "X") {
            return "X";
        }
        // Middle Row
        else if (buttonList[3].text == "X" && buttonList[4].text == "X" && buttonList[5].text == "X") {
            return "X";
        }
        // Bottom Row
        else if (buttonList[6].text == "X" && buttonList[7].text == "X" && buttonList[8].text == "X") {
            return "X";
        }
        // Left Column
        else if (buttonList[0].text == "X" && buttonList[3].text == "X" && buttonList[6].text == "X") {
            return "X";
        }
        // Middle Column
        else if (buttonList[1].text == "X" && buttonList[4].text == "X" && buttonList[7].text == "X") {
            return "X";
        }
        // Right Column
        else if (buttonList[2].text == "X" && buttonList[5].text == "X" && buttonList[8].text == "X") {
            return "X";
        }
        // Falling Diagonal
        else if (buttonList[0].text == "X" && buttonList[4].text == "X" && buttonList[8].text == "X") {
            return "X";
        }
        // Rising Diagonal
        else if (buttonList[6].text == "X" && buttonList[4].text == "X" && buttonList[2].text == "X") {
            return "X";
        }

        else if (moveCount >= 9) {
            return "Draw";
        }
        else {
            for (int i = 0; i < buttonList.Length; i++) {
                if (buttonList[i].text == "") {
                    return null;
                }
            }
        }
        return "Draw";
    }
}