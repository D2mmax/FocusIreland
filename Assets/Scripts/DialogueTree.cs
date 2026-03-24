using System;
using System.Collections.Generic;
using UnityEngine;

// ---------------------------------------------------------------------------
//  EHCEffect  — how one dialogue choice affects Energy, Hope, and Connection
// ---------------------------------------------------------------------------
[Serializable]
public class EHCEffect
{
    [Range(-20, 20)] public int energyDelta;
    [Range(-20, 20)] public int hopeDelta;
    [Range(-20, 20)] public int connectionDelta;
}

// ---------------------------------------------------------------------------
//  DialogueChoice  — a single selectable option inside a node
// ---------------------------------------------------------------------------
[Serializable]
public class DialogueChoice
{
    [Tooltip("Text shown on the choice button")]
    public string choiceText;

    [Tooltip("ID of the node to go to when this choice is picked. Leave empty to end conversation.")]
    public string nextNodeID;

    [Tooltip("EHC stat changes triggered by picking this choice")]
    public EHCEffect ehcEffect;
}

// ---------------------------------------------------------------------------
//  DialogueNode  — one beat of conversation
// ---------------------------------------------------------------------------
[Serializable]
public class DialogueNode
{
    [Tooltip("Unique ID for this node. Used by choices to link to it.")]
    public string nodeID;

    public enum Speaker { NPC, Player }

    [Tooltip("Who is speaking this line")]
    public Speaker speaker;

    [TextArea(2, 5)]
    [Tooltip("The dialogue line shown on screen")]
    public string dialogueText;

    [Tooltip("Choices the player can pick after this line. If empty, conversation ends.")]
    public List<DialogueChoice> choices = new List<DialogueChoice>();
}

// ---------------------------------------------------------------------------
//  DialogueTree  — ScriptableObject asset that holds a full conversation
//  Create via: Right-click in Project > Create > FocusIreland > Dialogue Tree
// ---------------------------------------------------------------------------
[CreateAssetMenu(fileName = "NewDialogueTree", menuName = "FocusIreland/Dialogue Tree")]
public class DialogueTree : ScriptableObject
{
    [Tooltip("ID of the node to start the conversation from")]
    public string startNodeID = "start";

    [Tooltip("All nodes in this conversation")]
    public List<DialogueNode> nodes = new List<DialogueNode>();

    // Lookup a node by its ID at runtime
    public DialogueNode GetNode(string id)
    {
        return nodes.Find(n => n.nodeID == id);
    }
}
