namespace AdventOfCode2022.Core.Days_10_19.Day_17;

public class Simulator {

    private readonly Chamber _chamber;
    private readonly RockFactory _rockFactory;
    private readonly JetGenerator _jetGenerator;

    public Simulator(Chamber chamber, RockFactory rockFactory, JetGenerator jetGenerator) {
        _chamber = chamber;
        _rockFactory = rockFactory;
        _jetGenerator = jetGenerator;
    }

    public void SimulateNextRock() {

        //3 empty spaces + one
        var towerHeight = _chamber.GetTowerHeight();
        var y = towerHeight + 4;
        var x = 2;

        var rock = _rockFactory.GenerateNext(new(x, y));
        rock = SimulateFallingRock(rock);

        var rockPositions = rock.EnumerateCurrentPositions();
        foreach(var position in rockPositions) {
            _chamber.AddRock(position);
        }

        //_chamber.RefreshFloor();
    }

    private Rock SimulateFallingRock(Rock rock) {

        while(true) {

            var topRocks = _chamber.GetTopRocks().ToArray();

            var jetDirection = _jetGenerator.Next();
            if(rock.LeftEdge + jetDirection >= 0 && (rock.RightEdge + jetDirection) < _chamber.ChamberWidth) {

                //test - revert if it collides
                rock.Position.X += jetDirection;
                foreach(var pillar in topRocks) {
                    if(rock.CollidesWithPillar(pillar)) {

                        //as it collides, revert the move
                        rock.Position.X -= jetDirection;
                        break;
                    }
                }

            }

            rock.Position.Y--;
            foreach(var pillar in topRocks) {
                
                if(!rock.CollidesWithPillar(pillar)) {
                    continue;
                }

                //found the floor
                rock.Position.Y++;
                return rock;
            }

        }

    }

    public void SimulateNextRock(ChamberGrid chamber) {

        //3 empty spaces + one
        var towerHeight = chamber.GetTowerHeight();
        var y = towerHeight + 4;
        var x = 2;

        var rock = _rockFactory.GenerateNext(new(x, y));
        rock = SimulateFallingRock(rock, chamber);

        var rockPositions = rock.EnumerateCurrentPositions();
        foreach(var position in rockPositions) {
            chamber.AddRock(position);
        }

        //_chamber.RefreshFloor();
    }

    private Rock SimulateFallingRock(Rock rock, ChamberGrid chamber) {

        while(true) {

            var nearRocks = chamber.EnumerateRocksAbove(rock.Position.Y - 1).ToArray();

            var jetDirection = _jetGenerator.Next();
            if(rock.LeftEdge + jetDirection >= 0 && (rock.RightEdge + jetDirection) < _chamber.ChamberWidth) {

                //test - revert if it collides
                rock.Position.X += jetDirection;
                if(nearRocks.Any(rock.CollidesWith)) {
                    rock.Position.X -= jetDirection;
                }

            }

            rock.Position.Y--;
            if(nearRocks.Any(rock.CollidesWith)) {

                rock.Position.Y++;
                return rock;

            }

        }

    }

}
