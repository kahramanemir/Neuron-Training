# Neural Network Training

## 📌 About The Project

This project implements a simple neural network training model using a perceptron-like approach. It takes in a dataset with two inputs and a target value (student study time, attendance, and actual grade) and adjusts weights using a basic gradient descent algorithm to minimize error.

## 🚀 Features

✅ Random weight initialization  
✅ Dataset normalization for better convergence  
✅ Training through iterative weight adjustment  
✅ Mean Squared Error (MSE) calculation  
✅ Console output for real-time results  
✅ User input for additional predictions  

## ⚡ Getting Started

To run this project, follow these steps:

1. Clone the repository  
2. Open the project in a C# development environment (e.g., Visual Studio)  
3. Run the `Program.cs` file  
4. Follow the console instructions for additional predictions  

## 📜 Algorithm Details

### 🔹 Dataset

The dataset consists of student study time, attendance, and actual grades:

```csharp
 double[,] dataSet = {{7.6, 11, 77},
                      {8, 10, 70},
                      {6.6, 8, 55},
                      {8.4, 10, 78},
                      {8.8, 12, 95}};
```

### 🔹 Normalization

```csharp
 dataSet[i, 0] = dataSet[i, 0] / 10;
 dataSet[i, 1] = dataSet[i, 1] / 15;
 dataSet[i, 2] = dataSet[i, 2] / 100;
```

### 🔹 Training

```csharp
 output[j] = weights[0] * dataSet[j, 0] + weights[1] * dataSet[j, 1];
 weights[0] = weights[0] + lambda * (dataSet[j, 2] - output[j]) * dataSet[j, 0];
 weights[1] = weights[1] + lambda * (dataSet[j, 2] - output[j]) * dataSet[j, 1];
```

### 🔹 Error Calculation (MSE)

```csharp
 double mse = mseHesapla(dataSet, output);
```

## 👥 Contributors

- Emir Kahraman
- Bülent Yıldırım

## 📄 License

This project is licensed under the MIT License.

## 📩 Contact

For any inquiries or contributions, feel free to open an issue or pull request on GitHub.
