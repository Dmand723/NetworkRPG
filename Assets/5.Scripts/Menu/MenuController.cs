using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class MenuController : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    #region componets
    [Header("Menu Screens")]
    public GameObject mainScreen;
    public GameObject createRoomScreen;
    public GameObject roomLobbyScreen;
    public GameObject roomSelectLobbyScreen;

    [Header("Main Screen Componets")]
    public TMP_InputField name_input;
    public Button createRoomButton_ms;
    public Button findRoomButton_ms;

    [Header("Create Room Screen Componets")]
    public TMP_InputField roomName_crs;
    public Button createRoomButton_crs;
    public Button backButton_crs;

    [Header("Room Lobby Components")]
    public Button startGameButton_rls;
    public TextMeshProUGUI playerList;
    public TextMeshProUGUI roomNamrText;
    public TMP_Dropdown levelSelect;

    [Header("Room Selection Lobby Componets")]
    public RectTransform roomListContainer;
    public GameObject roomButtonPrefab;

    [SerializeField]
    private List<GameObject> roomButtonList = new List<GameObject>();
    [SerializeField]
    private List<RoomInfo> roomInfoList = new List<RoomInfo>();
    #endregion

    #region unity Methods
    private void Start()
    {
        // making bttns non interacble until network is established
        createRoomButton_ms.interactable = false;
        findRoomButton_ms.interactable = false;

        // unhide our curor
        Cursor.lockState = CursorLockMode.None;

        // check if in game
        if(PhotonNetwork.InRoom)
        {
            // go to the lobby 

            // make room visible
            PhotonNetwork.CurrentRoom.IsVisible = true;
            PhotonNetwork.CurrentRoom.IsOpen = true;



        }
    }
    #endregion

    #region all Screen
    void setScreen(GameObject screen)
    {
        mainScreen.SetActive(false);
        createRoomScreen.SetActive(false);
        roomLobbyScreen.SetActive(false);
        roomSelectLobbyScreen.SetActive(false);

        screen.SetActive(true);
    }
    #endregion

    #region main Screen

    public void onPlayerNameValueChanged()
    {
        PhotonNetwork.NickName = name_input.text; 

        createRoomButton_ms.interactable = true;
        findRoomButton_ms.interactable = true;
    }

    #endregion

    #region create Room


    #endregion

    #region romm Lobby

    #endregion

    #region room Select

    #endregion
}

