using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AForge.Fuzzy;
using System;
using UnityEngine.AI;

public class EnemyFuzzy : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform player;
    float distace, speed;

    FuzzySet fsSlow, fsMedium, fsFast;
    LinguisticVariable lvSpeed;

    FuzzySet fsNear, fsMed, fsFar;
    LinguisticVariable lvDistance;
    
    Database database;
    InferenceSystem infSystem;
    
    void Start() {

        Initialize();
    }

    private void Initialize() {

        agent = GetComponent<NavMeshAgent>();
        SetDistanceFuzzySets();
        SetSpeedFuzzySets();
        AddToDatabese();
    }

    private void SetSpeedFuzzySets() {

        fsSlow = new FuzzySet("Slow", new TrapezoidalFunction(5, 7, TrapezoidalFunction.EdgeType.Right));
        fsMedium = new FuzzySet("Medium", new TrapezoidalFunction(6, 7, 8, 9));
        fsFast = new FuzzySet("Fast", new TrapezoidalFunction(7, 9, TrapezoidalFunction.EdgeType.Left));

        lvSpeed = new LinguisticVariable("Speed", 0, 10);
        lvSpeed.AddLabel(fsSlow);
        lvSpeed.AddLabel(fsMedium);
        lvSpeed.AddLabel(fsFast);
    }

    private void SetDistanceFuzzySets()  {

        fsNear = new FuzzySet("Near", new TrapezoidalFunction(3, 10, TrapezoidalFunction.EdgeType.Right));
        fsMed = new FuzzySet("Med", new TrapezoidalFunction(7, 9, 11, 13));
        fsFar = new FuzzySet("Far", new TrapezoidalFunction(10, 17, TrapezoidalFunction.EdgeType.Left));

        lvDistance = new LinguisticVariable("Distance", 0, 20);
        lvDistance.AddLabel(fsNear);
        lvDistance.AddLabel(fsMed);
        lvDistance.AddLabel(fsFar);
  
    }

    private void AddToDatabese()
    {
        database = new Database();
        database.AddVariable(lvDistance);
        database.AddVariable(lvSpeed);

        infSystem = new InferenceSystem(database, new CentroidDefuzzifier(20));
        infSystem.NewRule("Rule1", "IF Distance IS Near THEN Speed IS Slow");
        infSystem.NewRule("Rule2", "IF Distance IS Med THEN Speed IS Medium");
        infSystem.NewRule("Rule3", "IF Distance IS Far THEN Speed IS Fast");

    }
    
    void Update() {

        Evaluate();
    }

    private void Evaluate() {

        distace = Vector3.Distance(transform.position, player.position);
        infSystem.SetInput("Distance", distace);
        speed = infSystem.Evaluate("Speed");
        agent.speed = speed;

    }
}
