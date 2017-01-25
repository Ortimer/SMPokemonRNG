using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SMHatchingRNGTool {
  class Node: FastPriorityQueueNode {
    private int index;
    private int jump;
    private int distanceToHere = Int32.MaxValue;
    private List<Node> path = new List<Node>();

    public Node(int index, int jump) {
      this.index = index;
      this.jump = jump;
    }

    public int getJump() {
      return jump;
    }

    public int getDistanceToHere() {
      return distanceToHere;
    }

    public void setDistanceToHere(int distanceToHere) {
      this.distanceToHere = distanceToHere;
    }

    public int getIndex() {
      return index;
    }

    public List<Node> getPath() {
      return path;
    }

    public void setPath(List<Node> path) {
      this.path = path;
    }

    public String getPathDescription() {
      bool firstTime = true;
      bool isRejecting = false;
      int counter = 0;
      List<String> steps = new List<string>();
      Node startNode;
      Node nextNode;

      for (int i = 0; i < path.Count - 1; i++) {
        startNode = path[i];
        nextNode = path[i + 1];

        if (startNode.index + 1 != nextNode.index) {
          //It's accepting
          if (firstTime) {
            isRejecting = false;
            firstTime = false;
          }

          if (isRejecting) {
            if (counter != 0) {
              steps.Add("Reject " + counter + " eggs");
              counter = 1;
            }
            isRejecting = false;
          }
          else {
            counter++;
          }
        }
        else {
          if (firstTime) {
            isRejecting = true;
            firstTime = false;
          }

          //It's rejecting
          if (!isRejecting) {
            if (counter != 0) {
              steps.Add("Accept " + counter + " eggs");
              counter = 1;
            }
            isRejecting = true;
          }
          else {
            counter++;
          }
        }
      }

      startNode = path[path.Count - 1];
      nextNode = this;

      if (startNode.index + 1 != nextNode.index) {
        if (firstTime) {
          isRejecting = false;
          firstTime = false;
        }

        //It's accepting
        if (isRejecting) {
          if (counter != 0) {
            steps.Add("Reject " + counter + " eggs");
            counter = 1;
          }
          isRejecting = false;
        }
        else {
          counter++;
        }
      }
      else {
        if (firstTime) {
          isRejecting = true;
          firstTime = false;
        }

        //It's rejecting
        if (!isRejecting) {
          if (counter != 0) {
            steps.Add("Accept " + counter + " eggs");
            counter = 1;
          }
          isRejecting = true;
        }
        else {
          counter++;
        }
      }

      if (counter != 0) {
        if (isRejecting) {
          steps.Add("Reject " + counter + " eggs");
        }
        else {
          steps.Add("Accept " + counter + " eggs");
        }
      }

      String stringPath = "";
      foreach (String step in steps) {
        if(stringPath.Length > 0) { 
          stringPath += "\n then ";
          stringPath += step.ToLower();
        } else {
          stringPath = step;
        }
      }
      return stringPath;
    }
  }
}
