using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{

    [SerializeField] private EnemyManager _enemyManager;

    [SerializeField] private List<ContinuousEffect> _continuousEffectsApplied = new List<ContinuousEffect>();
    [SerializeField] private List<OneTimeEffect> _oneTimeEffectsApplied = new List<OneTimeEffect>();

    [SerializeField] private List<ContinuousEffect> _robotsEffect = new List<ContinuousEffect>();

    [SerializeField] private List<ContinuousEffect> _continuousEffects = new List<ContinuousEffect>();
    [SerializeField] private List<OneTimeEffect> _oneTimeEffects = new List<OneTimeEffect>();

    [SerializeField] private CardManager _cardManager;
    [SerializeField] private Player _player;

    [SerializeField] private TopIconManager _topIconManager;
    public Action OnHideCards;

    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioSource _clickLevelUp;
    [SerializeField] private AudioSource _levelUp;


    private void Awake()
    {
        // ��������� ������� �������, ���� �� �������� ���������
        for (int i = 0; i < _continuousEffects.Count; i++)
        {
            _continuousEffects[i] = Instantiate(_continuousEffects[i]);
            _continuousEffects[i].Initialize(this, _enemyManager, _player);
        }
        for (int i = 0; i < _oneTimeEffects.Count; i++)
        {
            _oneTimeEffects[i] = Instantiate(_oneTimeEffects[i]);
            _oneTimeEffects[i].Initialize(this, _enemyManager, _player);
        }
        for (int i = 0; i < _robotsEffect.Count; i++)
        {
            _robotsEffect[i] = Instantiate(_robotsEffect[i]);
            _robotsEffect[i].Initialize(this, _enemyManager, _player, _audioManager);
        }
    }

    //private void Start()
    //{
    //    AddRobotCard();
    //}
    [ContextMenu(nameof(ShowCards))]
    public void ShowCards(int level)
    {
        _levelUp.Play();
        // ���� ������� ����� ������, �� ����� �������� ������ ����� �����,
        // ����� ������ ����� ����� ���������
        bool onlyContinuous = level == 1;

        // List �������� �� �������� ����� ������� 3 ���������
        List<Effect> effectsToShow = new List<Effect>();

        // ����������� Continuous �������
        for (int i = 0; i < _continuousEffectsApplied.Count; i++)
        {
            if (_continuousEffectsApplied[i].Level < 10)
            {
                effectsToShow.Add(_continuousEffectsApplied[i]);
            }
        }

        // ����������� OneTime �������
        for (int i = 0; i < _oneTimeEffectsApplied.Count; i++)
        {
            if (_oneTimeEffectsApplied[i].Level < 10)
            {
                effectsToShow.Add(_oneTimeEffectsApplied[i]);
            }
        }

        // �� ����������� Continuous �������
        if (_continuousEffectsApplied.Count < 5)
        {
            effectsToShow.AddRange(_continuousEffects);
        }

        // �� ����������� Continuous �������
        if (_oneTimeEffectsApplied.Count < 4 && onlyContinuous == false)
        {
            effectsToShow.AddRange(_oneTimeEffects);
        }

        // ���������� ����, ������� ����� ��������.
        // ���� � ������ effectsToShow �� ����� ���������� ������ ��� 3
        int numverOfCardsToShow = Mathf.Min(effectsToShow.Count, 3);

        // ������������ ����� � ������� List effectsForCards,
        // � ������� ����� 3 ��������� ����� �� ����� effectsToShow
        int[] randomIndexes = RandomSort(effectsToShow.Count, numverOfCardsToShow);
        List<Effect> effectsForCards = new List<Effect>();
        for (int i = 0; i < randomIndexes.Length; i++)
        {
            int index = randomIndexes[i];
            effectsForCards.Add(effectsToShow[index]);
        }

        // �������� ����� ��� ������ � cardManager. level ����� ���� ������ ���������� ��� � ���� ������.
        _cardManager.ShowCards(effectsForCards, level);        
    }

    void HideCards()
    {
        _cardManager.HideCards();
        // ���� ������������� GameManager ����� ������� ����� ������ ����� �������� ���� � ����������
        OnHideCards.Invoke();
    }

    // ����� ����� length ����� � ���������� number ��������� �� ���
    int[] RandomSort(int length, int number)
    {
        int[] array = new int[length];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i;
        }
        for (int i = 0; i < array.Length; i++)
        {
            int oldValue = array[i];
            int newIndex = UnityEngine.Random.Range(0, array.Length);
            array[i] = array[newIndex];
            array[newIndex] = oldValue;
        }
        int[] result = new int[number];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = array[i];
        }
        return result;
    }


    public void AddRobotCard()
    {
        Effect effect = _robotsEffect[Progress.InstanceProgress.IndexRobot];
        
        if (effect is ContinuousEffect c_effect)
        {
            if (!_continuousEffectsApplied.Contains(c_effect))
            {
                _continuousEffectsApplied.Add(c_effect);
                _robotsEffect.Remove(c_effect);
                _topIconManager.AddIcon(c_effect);

                c_effect._audioManager = _audioManager;
            }

        }
        effect.Activate();
    }
    // ���������� ��� ����� �� �����
    public void ClickCard(Effect effect)
    {
        // ���������� ������� �� ������ �� ����������� � ������ ����������
        if (effect is ContinuousEffect c_effect)
        {
            if (!_continuousEffectsApplied.Contains(c_effect))
            {
                _continuousEffectsApplied.Add(c_effect);
                _continuousEffects.Remove(c_effect);
                _topIconManager.AddIcon(c_effect);

                c_effect._audioManager = _audioManager;
            }

        }
        else if (effect is OneTimeEffect o_effect)
        {
            if (!_oneTimeEffectsApplied.Contains(o_effect))
            {
                _oneTimeEffectsApplied.Add(o_effect);
                _oneTimeEffects.Remove(o_effect);
                _topIconManager.AddIcon(o_effect);
            }
        }

        // ���������� ������
        effect.Activate();
        _clickLevelUp.Play();
        HideCards();
    }

    void Update()
    {
        foreach (var effect in _continuousEffectsApplied)
        {
            effect.ProcessFrame(Time.deltaTime * (1 + _player.ColldownReduction));
        }
    }


}
