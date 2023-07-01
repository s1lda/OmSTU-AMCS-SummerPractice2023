using spacebattle;
using System;
using TechTalk.SpecFlow;

namespace spacebattletests.StepDefinitions
{
    [Binding]
    public class MovingActionStepDefinitions
    {
        Spaceship spaceship = new Spaceship();
        int[]? actual_coordinates;
        Exception exception = new Exception();

        [Given(@"����������� ������� ��������� � ����� ������������ � ������������ \((.*), (.*)\)")]
        public void GivenCoordinates(int p0, int p1)
        {
            spaceship.SetCoordinates(p0, p1);
        }

        [Given(@"����� ���������� �������� \((.*), (.*)\)")]
        public void GivenSpeedVector(int p0, int p1)
        {
            spaceship.SetSpeedVector(p0, p1);
        }

        [Given(@"����������� �������, ��������� � ������������ �������� ���������� ����������")]
        public void GivenImpossibleCoordinates()
        {

        }

        [Given(@"�������� ������� ���������� ����������")]
        public void GivenImpossibleSpeedVector()
        {

        }

        [Given(@"�������� ��������� � ������������ ������������ ������� ����������")]
        public void GivenImpossibleAction()
        {
            spaceship.CannotAction();
        }

        [When(@"���������� ������������� ����������� �������� ��� ����������")]
        public void WhenMovingAction()
        {
            try
            {
                actual_coordinates = spaceship.MovingAction();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        [Then(@"����������� ������� ������������ � ����� ������������ � ������������ \((.*), (.*)\)")]
        public void ThenSpaceshipMovingToCoordinates(int p0, int p1)
        {
            Assert.Equal(new int[] { p0, p1 }, actual_coordinates);
        }

        [Then(@"��������� ������ Exception")]
        public void ThenThrowsException()
        {
            Assert.Equal("coordinates or speed vector was null", exception.Message);
        }
    }
}